using Artexitus.IdentityMicroservice.Contracts.DTO;
using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Queries.User
{
    public record GetUserByIdQuery : IRequest<UserDTO>
    {
        public required Guid Id { get; init; }
    }
}
