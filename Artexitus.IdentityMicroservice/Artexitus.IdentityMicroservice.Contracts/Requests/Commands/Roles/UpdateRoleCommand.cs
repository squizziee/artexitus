using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Roles
{
    public class UpdateRoleCommand : IRequest
    {
        [FromRoute]
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
    }
}
