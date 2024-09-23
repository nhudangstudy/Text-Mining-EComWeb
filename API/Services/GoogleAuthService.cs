using Google.Apis.Util;
using Common.IRepositories;
using Common.IServices;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace API.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly Config _config;

        public GoogleAuthService(Config config) => _config = config;

        public async Task<string> GetEmail(string accessToken)
        {
            var validationSettings = new ValidationSettings
            {
                Clock = SystemClock.Default,
                ExpirationTimeClockTolerance = TimeSpan.FromDays(30),
                IssuedAtClockTolerance = TimeSpan.FromDays(30),
                Audience = new string[] { _config.GoogleClientId }
            };

            var payload = await ValidateAsync(accessToken, validationSettings);

            return payload.Email;
        }
    }
}
