namespace Artexitus.IdentityMicroservice.Domain.Entities
{
    public class UserProfile : EntityBase
    {
        public string Username { get; set; } = string.Empty;
        public User User { get; set; }
        public Guid RoleId { get; set; }
        public UserRole Role { get; set; }
    }
}
