using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Application.Services;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Helpers;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands;
using MediatR;

namespace Artexitus.IdentityMicroservice.Application.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, UserTokens>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHashingService _passwordHashingService;

        public LoginUserHandler(IUserRepository userRepository, 
            ITokenService tokenService,
            IPasswordHashingService passwordHashingService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHashingService = passwordHashingService;
        }

        public async Task<UserTokens> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var tryFind = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (tryFind == null)
            {
                throw new ResourceDoesNotExistException($"User with email {request.Email} does not exist. Unable to log in");
            }

            if (!_passwordHashingService.ValidatePassword(request.Password, tryFind.PasswordHash))
            {
                throw new InvalidCredentialsException($"Wrong password for user with email {request.Email}. Unable to log in");
            }

            var tokens = new UserTokens
            {
                AccessToken = _tokenService.GenerateAccessToken(tryFind),
                RefreshToken = _tokenService.GenerateRefreshToken(tryFind),
            };

            tryFind.RefreshToken = tokens.RefreshToken;

            await _userRepository.SaveChangesAsync(cancellationToken);

            return tokens;
        }
    }
}
