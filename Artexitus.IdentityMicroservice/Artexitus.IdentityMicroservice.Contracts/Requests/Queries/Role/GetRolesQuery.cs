using Artexitus.IdentityMicroservice.Contracts.DTO;
using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Queries.Roles
{
    public record GetRolesQuery : IRequest<IEnumerable<RoleDTO>>
    {
    }
}
