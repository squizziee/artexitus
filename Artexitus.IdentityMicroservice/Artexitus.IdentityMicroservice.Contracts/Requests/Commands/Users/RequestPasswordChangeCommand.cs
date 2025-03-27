using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users
{
    public class RequestPasswordChangeCommand : IRequest
    {
        public required string Email { get; init; }
    }
}
