using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Roles;
using Artexitus.IdentityMicroservice.Domain.Entities;
using MediatR;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Roles
{
    public class AddRoleHandler : IRequestHandler<AddRoleCommand>
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public AddRoleHandler(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;   
        }

        public async Task Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var newRole = new UserRole
            {
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
            };

            await _userRoleRepository.AddAsync(newRole, cancellationToken);
        }
    }
}
