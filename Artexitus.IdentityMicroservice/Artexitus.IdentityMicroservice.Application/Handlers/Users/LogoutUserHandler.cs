using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Application.Services;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Users
{
    public class LogoutUserHandler : IRequestHandler<LogoutUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<ActivateAccountHandler> _logger;

        public LogoutUserHandler(IUserRepository userRepository,
            ITokenService tokenService,
            ILogger<ActivateAccountHandler> logger)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user == null)
            {
                throw new ResourceDoesNotExistException($"No user with id {request.Id} was found. Unable to log out");
            }

            user.RefreshToken = _tokenService.GenerateRefreshToken(user);

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("User with email {email} logged out", user.Email);
        }
    }
}
