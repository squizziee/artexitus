namespace Artexitus.IdentityMicroservice.Application.Handlers.Users.Commands
{
    internal record PasswordChangeRequest
    {
        public required Guid UserId { get; init; }
    }
}
