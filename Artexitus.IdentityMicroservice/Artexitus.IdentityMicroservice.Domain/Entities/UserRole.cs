namespace Artexitus.IdentityMicroservice.Domain.Entities
{
    public class UserRole : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
