using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Artexitus.IdentityMicroservice.Application.Handlers
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

            await _userRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Account with email {email} was successfully activated", tryFind.Email);
        }
    }
}
