namespace Artexitus.IdentityMicroservice.Application.Handlers.Users
{
    internal record PasswordChangeRequest
    {
        public required Guid UserId { get; init; }
    }
}
