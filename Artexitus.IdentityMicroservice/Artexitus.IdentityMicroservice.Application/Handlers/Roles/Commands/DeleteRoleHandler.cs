using Artexitus.IdentityMicroservice.Domain.Repositories;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Roles;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Roles.Commands
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ILogger<DeleteRoleHandler> _logger;

        public DeleteRoleHandler(IUserRoleRepository userRoleRepository,
            ILogger<DeleteRoleHandler> logger)
        {
            _userRoleRepository = userRoleRepository;
            _logger = logger;
        }

        public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _userRoleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (role == null)
            {
                throw new ResourceDoesNotExistException($"Role with id {request.Id} does not exist. Unable to delete");
            }

            await _userRoleRepository.DeleteAsync(role, cancellationToken);
            await _userRoleRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Role deleted: {name}", role.Name);
        }
    }
}
