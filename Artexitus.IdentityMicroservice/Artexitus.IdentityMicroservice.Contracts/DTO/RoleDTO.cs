namespace Artexitus.IdentityMicroservice.Contracts.DTO
{
    public record RoleDTO
    {
        public required Guid id { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
    }
}
