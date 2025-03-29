using Artexitus.IdentityMicroservice.Domain.Repositories;
using Artexitus.IdentityMicroservice.Application.Services;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Helpers;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Users.Commands
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, UserTokens>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly ILogger<LoginUserHandler> _logger;

        public LoginUserHandler(IUserRepository userRepository,
            ITokenService tokenService,
            IPasswordHashingService passwordHashingService,
            ILogger<LoginUserHandler> logger)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHashingService = passwordHashingService;
            _logger = logger;
        }

        public async Task<UserTokens> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user == null)
            {
                throw new ResourceDoesNotExistException($"User with email {request.Email} does not exist. Unable to log in");
            }

            if (!_passwordHashingService.ValidatePassword(request.Password, user.PasswordHash))
            {
                throw new InvalidCredentialsException($"Wrong password for user with email {request.Email}. Unable to log in");
            }

            // Done in case of long inactivity, so the account could be easily recovered
            var isStale = user.DeletedAt != null;

            if (isStale)
            {
                user.DeletedAt = null;
            }

            var tokens = new UserTokens
            {
                AccessToken = _tokenService.GenerateAccessToken(user),
                RefreshToken = _tokenService.GenerateRefreshToken(user),
            };

            user.RefreshToken = tokens.RefreshToken;
            user.LastRefresh = DateTimeOffset.UtcNow;

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            if (isStale)
            {
                _logger.LogInformation("User account recovered: {email}", user.Email);
            }

            _logger.LogInformation("User account sign-in: {email}", user.Email);


            return tokens;
        }
    }
}
