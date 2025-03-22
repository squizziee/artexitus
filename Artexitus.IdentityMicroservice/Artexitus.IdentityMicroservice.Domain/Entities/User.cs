namespace Artexitus.IdentityMicroservice.Domain.Entities
{
    public class User : EntityBase
    {
        public string Email { get; set; } = string.Empty;
        public Guid ProfileId { get; set; }
        public UserProfile Profile { get; set; }
        public string PasswordHash { get; set; } = string.Empty;

    }
}
