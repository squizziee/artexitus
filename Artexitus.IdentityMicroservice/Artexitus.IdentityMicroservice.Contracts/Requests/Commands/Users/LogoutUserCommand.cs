using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users
{
    public class LogoutUserCommand : IRequest
    {
        public required Guid Id { get; init; }
    }
}
