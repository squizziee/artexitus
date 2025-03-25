using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Roles;
using MediatR;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Roles
{
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand>
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UpdateRoleHandler(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
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
        }
    }
}
