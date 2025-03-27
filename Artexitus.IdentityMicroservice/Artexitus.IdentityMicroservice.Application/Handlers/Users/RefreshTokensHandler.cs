using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Application.Services;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Helpers;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Users
{
    public class RefreshTokensHandler : IRequestHandler<RefreshTokensCommand, UserTokens>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<RefreshTokensHandler> _logger;

        public RefreshTokensHandler(IUserRepository userRepository,
            ITokenService tokenService,
            ILogger<RefreshTokensHandler> logger)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<UserTokens> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken, cancellationToken);

            if (user == null)
            {
                throw new ResourceDoesNotExistException($"User with refresh token {request.RefreshToken} does not exist. Unable to refresh");
            }

            var tokens = new UserTokens
            {
                AccessToken = _tokenService.GenerateAccessToken(user),
                RefreshToken = _tokenService.GenerateRefreshToken(user),
            };

            user.RefreshToken = tokens.RefreshToken;

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("User account token refresh: {email}", user.Email);

            return tokens;
        }
    }
}
