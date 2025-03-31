namespace Artexitus.IdentityMicroservice.Domain.Entities
{
    public class User : EntityBase
    {
        public string Email { get; set; } = string.Empty;
        public Guid ProfileId { get; set; }
        public UserProfile Profile { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string? ActivationToken { get; set; }
        public bool IsActivated { get; set; }
        public DateTimeOffset LastRefresh { get; set; }
        public DateTimeOffset ActivationTokenValidTo { get; set; }

    }
}
