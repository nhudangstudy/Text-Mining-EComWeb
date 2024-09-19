namespace API
{
    public sealed record Config
    {
        public byte[] SecretKey { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public TimeSpan AccessTokenLifeTime { get; init; }
        public TimeSpan RefreshTokenLifeTime { get; init; }
        public string GoogleClientId { get; init; }

        public record Smtp
        {
            public string Email { get; init; }
            public string DisplayName { get; init; }
            public string Password { get; init; }
            public string Host { get; init; }
            public int Port { get; init; }
        }
        public Smtp SmtpNoreply { get; init; }

        public record Aes
        {
            public byte[] Key { get; init; }
            public byte[] IV { get; init; }
        }
        public Aes AesCrypto { get; init; }
    }
}
