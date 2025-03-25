using Artexitus.IdentityMicroservice.Contracts.DTO;
using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Queries.User
{
    public record GetUserByUsernameQuery : IRequest<UserDTO>
    {
        public required string Username { get; set; }
    }
}
