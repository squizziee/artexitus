using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Application.Services;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users;
using Artexitus.IdentityMicroservice.Domain.Entities;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Users
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly ILogger<RegisterUserHandler> _logger;

        public RegisterUserHandler(IUserRepository userRepository,
            IUserProfileRepository userProfileRepository,
            IUserRoleRepository userRoleRepository,
            IPasswordHashingService passwordHashingService,
            ITokenService tokenService,
            IEmailService emailService,
            ILogger<RegisterUserHandler> logger)
        {
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _userRoleRepository = userRoleRepository;
            _passwordHashingService = passwordHashingService;
            _tokenService = tokenService;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var tryFindByEmail = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (tryFindByEmail != null)
            {
                throw new AlreadyExistsException($"User with email {request.Email} is already present. Unable to register");
            }

            var tryFindByUsername = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken);

            if (tryFindByUsername != null)
            {
                throw new AlreadyExistsException($"User with username {request.Username} is already present. Unable to register");
            }

            var defaultRole = await _userRoleRepository.GetDefaultRoleAsync(cancellationToken);

            var profile = new UserProfile
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                RoleId = defaultRole.Id,
                Role = defaultRole,
            };

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Profile = profile,
                PasswordHash = _passwordHashingService.HashPassword(request.Password),
                IsActivated = false,
            };

            user.RefreshToken = _tokenService.GenerateRefreshToken(user);
            user.ActivationToken = _tokenService.GenerateActivationToken(user);

            await _userProfileRepository.AddAsync(profile, cancellationToken);
            await _userRepository.AddAsync(user, cancellationToken);

            // Redunant as SaveChangesAsync saves all changes in context
            // await _userProfileRepository.SaveChangesAsync(cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            BackgroundJob.Enqueue(
                () => _emailService.SendAccountActivationEmail(user, cancellationToken)
            );

            _logger.LogInformation("Registered new account with email {email} and username {username}",
                user.Email, profile.Username);
        }
    }
}
