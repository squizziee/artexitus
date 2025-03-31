using Artexitus.IdentityMicroservice.Domain.Repositories;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Roles;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Roles.Commands
{
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ILogger<UpdateRoleHandler> _logger;

        public UpdateRoleHandler(IUserRoleRepository userRoleRepository,
            ILogger<UpdateRoleHandler> logger)
        {
            _userRoleRepository = userRoleRepository;
            _logger = logger;
        }

        public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _userRoleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (role == null)
            {
                throw new ResourceDoesNotExistException($"Role with id {request.Id} does not exist. Unable to update");
            }

            role.Name = request.Name;
            role.Description = request.Description;

            await _userRoleRepository.UpdateAsync(role, cancellationToken);
            await _userRoleRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Role updated: guid {guid}", role.Id);
        }
    }
}
