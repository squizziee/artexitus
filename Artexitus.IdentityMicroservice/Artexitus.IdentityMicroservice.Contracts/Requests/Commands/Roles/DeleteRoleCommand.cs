using MediatR;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Roles
{
    public record DeleteRoleCommand : IRequest
    {
        public required Guid Id { get; init; }
        public bool IsSoftDelete { get; init; } = true;
    }
}
