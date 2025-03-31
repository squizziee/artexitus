using Artexitus.IdentityMicroservice.Domain.Repositories;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Users.Commands
{
    public class DeactivateAccountHandler : IRequestHandler<DeactivateAccountCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<DeactivateAccountHandler> _logger;

        public DeactivateAccountHandler(IUserRepository userRepository,
            ILogger<DeactivateAccountHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Handle(DeactivateAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user == null)
            {
                throw new ResourceDoesNotExistException($"No user with id {request.Id} was found. Unable to deactivate");
            }

            await _userRepository.SoftDeleteAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("User account deactivated: {email}", user.Email);
        }
    }
}
