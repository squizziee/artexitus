using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands
{
    public record DeleteUserCommand : IRequest
    {
        public required Guid UserId { get; init; }
    }
}
