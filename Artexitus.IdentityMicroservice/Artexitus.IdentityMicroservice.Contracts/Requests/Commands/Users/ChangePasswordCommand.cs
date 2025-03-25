using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users
{
    public record ChangePasswordCommand : IRequest
    {
        public required string PasswordChangeToken { get; init; }
        public required Guid UserId { get; init; }
    }
}
