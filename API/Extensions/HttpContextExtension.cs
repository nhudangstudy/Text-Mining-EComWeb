using System.Net;
using System.Security.Claims;

namespace API.Extensions
{
    public static class HttpContextExtension
    {
        public static string? GetClaim(this HttpContext httpContext, string type)
        {
            if (type is null)
            {
                return null;
            }

            return httpContext.User.FindFirst(p => p.Type == type)?.Value;
        }

        public static string GetUserId(this HttpContext httpContext) => GetClaim(httpContext, ClaimTypes.NameIdentifier)!;

        public static IPAddress? GetIP(this HttpContext httpContext)
            => httpContext.Connection?.RemoteIpAddress!;
    }
}
