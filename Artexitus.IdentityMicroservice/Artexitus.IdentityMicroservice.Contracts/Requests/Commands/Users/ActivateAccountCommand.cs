using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users
{
    public record ActivateAccountCommand : IRequest
    {
        public required string ActivationToken { get; init; }
    }
}
