namespace Artexitus.IdentityMicroservice.Infrastructure.ConfigurationSections
{
    public record ActivationTokenSettings
    {
        public required string Issuer { get; init; }
        public required string Audience { get; init; }
        public required int ExpirationTimeInHours { get; init; }
        public required string Key { get; init; }
    }
}
