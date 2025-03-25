using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Roles;
using MediatR;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Roles
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public DeleteRoleHandler(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _userRoleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (role == null)
            {
                throw new ResourceDoesNotExistException($"Role with id {request.Id} does not exist. Unable to delete");
            }

            await _userRoleRepository.DeleteAsync(role, cancellationToken);
        }
    }
}
