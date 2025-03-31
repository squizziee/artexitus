using Artexitus.IdentityMicroservice.Contracts.Helpers;
using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users
{
    public record RegisterUserCommand : IRequest
    {
        public required string Email { get; init; }
        public required string Username { get; init; }
        public required string Password { get; init; }
    }
}
