using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands
{
    public record ChangePasswordCommand : IRequest
    {
        public required string PasswordChangeToken { get; init; }
        public required Guid UserId { get; init; }
    }
}
