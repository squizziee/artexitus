using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users
{
    public record DeleteUserCommand : IRequest
    {
        public required Guid Id { get; init; }
    }
}
