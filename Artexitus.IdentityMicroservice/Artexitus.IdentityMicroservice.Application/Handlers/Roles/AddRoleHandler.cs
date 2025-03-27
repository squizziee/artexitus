using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Roles;
using Artexitus.IdentityMicroservice.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Roles
{
    public class AddRoleHandler : IRequestHandler<AddRoleCommand>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ILogger<AddRoleHandler> _logger;

        public AddRoleHandler(IUserRoleRepository userRoleRepository,
            ILogger<AddRoleHandler> logger)
        {
            _userRoleRepository = userRoleRepository;   
            _logger = logger;
        }

        public async Task Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new UserRole
            {
                Name = request.Name,
                Description = request.Description,
            };

            await _userRoleRepository.AddAsync(role, cancellationToken);
            await _userRoleRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("New role added: {name}", role.Name);
        }
    }
}
