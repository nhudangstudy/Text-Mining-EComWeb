using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace API.Cores
{
    public static class AuthenticationCore
    {
        public static Action<JwtBearerOptions> Options(Config config)
        {
            return options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(config.SecretKey),
        ValidIssuer = config.Issuer,
        ValidAudience = config.Audience,
        ClockSkew = TimeSpan.FromSeconds(10)
    };
    options.Events = new()
    {
        OnChallenge = context =>
        {
            string? clientToken = context.Request.Headers["Authorization"].ToString().Trim();
            var schemaToken = (JwtBearerDefaults.AuthenticationScheme, StringComparison.OrdinalIgnoreCase);
            switch (clientToken)
            {
                case "":
                case var c when c.Equals(schemaToken.Item1, schemaToken.Item2):
                    context.HandleResponse();
                    context.Response.StatusCode = 401;
                    return context.Response.WriteAsJsonAsync(ProblemModel.Unauthorized);
                case var c when !c.StartsWith(schemaToken.Item1, schemaToken.Item2):
                    context.HandleResponse();
                    context.Response.StatusCode = 412;
                    return context.Response.WriteAsJsonAsync(ProblemModel.AcessTokenFormatWrong);
            }

            context.HandleResponse();
            context.Response.StatusCode = 401;
            return context.AuthenticateFailure switch
            {
                var c when c is SecurityTokenInvalidSignatureException
        => context.Response.WriteAsJsonAsync(ProblemModel.InvalidToken),
                var c when c is SecurityTokenExpiredException
        => context.Response.WriteAsJsonAsync(ProblemModel.InvalidToken with { Detail = $"The access token provided is expired at {(c as SecurityTokenExpiredException)!.Expires.ToLocalTime()}." }),
                _
        => context.Response.WriteAsJsonAsync(ProblemModel.InvalidToken with { Detail = string.IsNullOrWhiteSpace(context.ErrorDescription) ? "Your access token is invalid." : context.ErrorDescription })
            };
        },
        OnForbidden = context =>
        {
            context.Response.StatusCode = 403;
            return context.Response.WriteAsJsonAsync(ProblemModel.Forbidden with { Instance = context.HttpContext.Request.GetDisplayUrl() });
        }
    };
};
        }
    }
}