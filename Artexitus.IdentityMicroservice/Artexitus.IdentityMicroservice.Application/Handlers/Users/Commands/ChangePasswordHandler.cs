using Artexitus.IdentityMicroservice.Domain.Repositories;
using Artexitus.IdentityMicroservice.Application.Services;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Users.Commands
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly ITokenService _tokenService;
        private readonly ICacheAccessor _cacheAccessor;
        private readonly ILogger<ChangePasswordHandler> _logger;

        public ChangePasswordHandler(IUserRepository userRepository,
            IPasswordHashingService passwordHashingService,
            ITokenService tokenService,
            ICacheAccessor cacheAccessor,
            ILogger<ChangePasswordHandler> logger)
        {
            _userRepository = userRepository;
            _passwordHashingService = passwordHashingService;
            _tokenService = tokenService;
            _cacheAccessor = cacheAccessor;
            _logger = logger;
        }

        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var changePasswordRequest = _cacheAccessor.Get<PasswordChangeRequest>($"pw_reset_{request.PasswordChangeToken}");

            if (changePasswordRequest == null)
            {
                throw new ResourceDoesNotExistException($"Password change request for {request.PasswordChangeToken} timed out. Unable to change password");
            }

            var user = await _userRepository.GetByIdAsync(changePasswordRequest.UserId, cancellationToken);

            if (user == null)
            {
                throw new ResourceDoesNotExistException($"No user with id {changePasswordRequest.UserId} was found. Unable to request password reset");
            }

            user.PasswordHash = _passwordHashingService.HashPassword(request.NewPassword);
            user.RefreshToken = _tokenService.GenerateRefreshToken(user);

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Password for {email} successfully changed. Refresh token has been revoked", user.Email);
        }
    }
}
