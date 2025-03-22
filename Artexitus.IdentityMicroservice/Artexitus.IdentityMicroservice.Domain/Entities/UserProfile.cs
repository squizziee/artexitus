namespace Artexitus.IdentityMicroservice.Domain.Entities
{
    public class UserProfile : EntityBase
    {
        public string Username { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
