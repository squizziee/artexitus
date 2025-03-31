using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Roles
{
    public record AddRoleCommand : IRequest
    {
        public required string Name { get; init; }
        public required string Description { get; init; }
    }
}
