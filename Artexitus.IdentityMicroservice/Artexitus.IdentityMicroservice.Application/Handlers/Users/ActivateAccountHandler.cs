using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Users
{
    public class ActivateAccountHandler : IRequestHandler<ActivateAccountCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ActivateAccountHandler> _logger;

        public ActivateAccountHandler(IUserRepository userRepository,
            ILogger<ActivateAccountHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
        {
            var tryFind = await _userRepository
                .GetByActivationTokenAsync(request.ActivationToken, cancellationToken);

            if (tryFind == null)
            {
                throw new ResourceDoesNotExistException($"No user with activation token {request.ActivationToken} " +
                    $"was found. Unable to activate");
            }

            tryFind.ActivationToken = null;
            tryFind.IsActivated = true;

            await _userRepository.UpdateAsync(tryFind, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Account {email} was successfully activated", tryFind.Email);
        }
    }
}
