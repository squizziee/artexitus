using Artexitus.IdentityMicroservice.Contracts.Helpers;
using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands
{
    public record LoginUserCommand : IRequest<UserTokens>
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
