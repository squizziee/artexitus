namespace Artexitus.IdentityMicroservice.Infrastructure.ConfigurationSections
{
    public record RefreshTokenSettings 
    {
        public required string Issuer { get; init; }
        public required string Audience { get; init; }
        public required int ExpirationTimeInDays { get; init; }
        public required string Key { get; init; }
    }
}
