using Artexitus.IdentityMicroservice.Contracts.DTO;
using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Queries
{
    public record GetUsersQuery : IRequest<PaginatedResponse<UserDTO>>
    {
        public required int PageNumber { get; set; }
    }
}
