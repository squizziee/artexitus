using Artexitus.IdentityMicroservice.Contracts.Helpers;
using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands
{
    public record RefreshTokensCommand : IRequest<UserTokens>
    {
        public required string RefreshToken { get; init; }
    }
}
