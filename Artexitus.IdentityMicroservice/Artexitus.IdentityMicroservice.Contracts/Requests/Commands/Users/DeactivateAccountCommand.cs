using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users
{
    public record DeactivateAccountCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
