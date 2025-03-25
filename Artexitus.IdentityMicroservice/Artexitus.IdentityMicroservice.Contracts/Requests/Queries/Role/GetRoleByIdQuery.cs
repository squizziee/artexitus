using Artexitus.IdentityMicroservice.Contracts.DTO;
using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Queries.Roles
{
    public record GetRoleByIdQuery : IRequest<RoleDTO>
    {
        public required Guid Id { get; init; }
    }
}
