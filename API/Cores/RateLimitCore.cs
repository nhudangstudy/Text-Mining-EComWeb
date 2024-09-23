using AspNetCoreRateLimit;

namespace API.Cores
{
    public static class RateLimitCore
    {
        public static Action<IpRateLimitOptions> Options = (options) =>
        {
            options.EnableEndpointRateLimiting = true;
            options.StackBlockedRequests = false;
            options.RealIpHeader = "X-Real-IP";
            options.ClientIdHeader = "X-ClientId";
            options.HttpStatusCode = StatusCodes.Status429TooManyRequests;

            options.GeneralRules = new List<RateLimitRule>
                {                    
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        PeriodTimespan = TimeSpan.FromSeconds(20),
                        Limit = 120
                    },
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        PeriodTimespan = TimeSpan.FromMinutes(5),
                        Limit = 600
                    },                    
                    new RateLimitRule
                    {
                        Endpoint = "GET:/api/team-final-round-bis",
                        PeriodTimespan = TimeSpan.FromSeconds(1),
                        Limit = 5
                    },
                    new RateLimitRule
                    {
                        Endpoint = "GET:/api/team-final-round-bis/*",
                        PeriodTimespan = TimeSpan.FromSeconds(1),
                        Limit = 5
                    }
                };
        };
    }
}
