namespace Artexitus.IdentityMicroservice.Infrastructure.ConfigurationSections
{
    public record AccessTokenSettings
    {
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required int ExpirationTimeInMinutes { get; set; }
        public required string Key { get; set; }
    }
}
