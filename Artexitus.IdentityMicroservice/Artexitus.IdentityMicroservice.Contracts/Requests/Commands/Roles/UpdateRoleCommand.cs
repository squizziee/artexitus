using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Roles
{
    public class UpdateRoleCommand : IRequest
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
    }
}
