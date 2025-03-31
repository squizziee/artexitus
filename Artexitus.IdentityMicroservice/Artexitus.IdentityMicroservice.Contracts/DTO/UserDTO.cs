namespace Artexitus.IdentityMicroservice.Contracts.DTO
{
    public record UserDTO
    {
        public required Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Role { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
    }
}
