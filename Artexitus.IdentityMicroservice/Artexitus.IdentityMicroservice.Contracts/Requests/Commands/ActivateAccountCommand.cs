using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands
{
    public record ActivateAccountCommand : IRequest
    {
        public required string ActivationToken { get; init; }
    }
}
