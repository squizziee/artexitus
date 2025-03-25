using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands
{
    public record DeactivateAccountCommand : IRequest
    {
        public required Guid UserId { get; set; }
    }
}
