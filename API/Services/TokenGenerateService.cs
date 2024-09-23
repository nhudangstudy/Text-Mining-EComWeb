using Microsoft.IdentityModel.Tokens;
using Common.IRepositories;
using Common.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Services
{
    public class TokenGenerateService : ITokenGenerateService
    {
        private readonly Config _config;

        public TokenGenerateService(Config config) => _config = config;

        private SecurityTokenDescriptor GenerateSecurityTokenDescriptor(GetClaimModel getClaim, TimeSpan duration)
        {
            var currentTime = DateTime.UtcNow;

            return new()
            {
                Subject = new(
                    getClaim.Scopes?.Select(p => new Claim(ClaimTypes.Role, p))
                        .Append(new(ClaimTypes.NameIdentifier, getClaim.ClientId))),
                Audience = _config.Audience,
                Issuer = _config.Issuer,
                NotBefore = currentTime,
                IssuedAt = currentTime,
                Expires = currentTime.Add(duration)
            };
        }

        public AccessTokenModel GenerateAccessToken(GetClaimModel getClaim)
        {
            var descriptor = GenerateSecurityTokenDescriptor(getClaim, _config.AccessTokenLifeTime);
            descriptor.TokenType = "access";
            descriptor.SigningCredentials = new(new SymmetricSecurityKey(_config.SecretKey), SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityTokenHandler().CreateEncodedJwt(descriptor);

            return new()
            {
                Value = token,
                Expire = descriptor.Expires!.Value
            };
        }

        public RefreshTokenModel GenerateRefreshToken()
            => new()
            {
                Value = Guid.NewGuid(),
                Expire = DateTime.UtcNow.Add(_config.RefreshTokenLifeTime)
            };
    }
}
