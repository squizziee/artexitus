namespace Artexitus.IdentityMicroservice.Application.ConfigurationSections
{
    public record PasswordResetSettings
    {
        public required int MinutesToResetPassword { get; init; }
    }
}
