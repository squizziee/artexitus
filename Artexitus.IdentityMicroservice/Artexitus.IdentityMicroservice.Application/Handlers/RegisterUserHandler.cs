using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Application.Services;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands;
using Artexitus.IdentityMicroservice.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Artexitus.IdentityMicroservice.Application.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        private readonly IPasswordHashingService _passwordHashingService;
        private readonly ITokenService _tokenService;

        private readonly ILogger<RegisterUserHandler> _logger;

        public RegisterUserHandler(IUserRepository userRepository, 
            IUserProfileRepository userProfileRepository, 
            IUserRoleRepository userRoleRepository,
            IPasswordHashingService passwordHashingService,
            ITokenService tokenService,
            ILogger<RegisterUserHandler> logger)
        {
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _userRoleRepository = userRoleRepository;
            _passwordHashingService = passwordHashingService;
            _tokenService = tokenService;
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

            var newProfile = new UserProfile
            {
                Username = request.Username,
                Role = defaultRole,
            };

            var newUser = new User
            {
                Email = request.Email,
                Profile = newProfile,
                PasswordHash = _passwordHashingService.HashPassword(request.Password),
                IsActivated = true,
            };

            newUser.RefreshToken = _tokenService.GenerateRefreshToken(newUser);
            //newUser.ActivationToken = _tokenService.GenerateActivationToken(newUser);

            await _userProfileRepository.AddAsync(newProfile, cancellationToken);
            await _userRepository.AddAsync(newUser, cancellationToken);

            // Redunant as SaveChangesAsync saves all changes in context
            // await _userProfileRepository.SaveChangesAsync(cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Registered new account with email {email} and username {username}", 
                newUser.Email, newProfile.Username);
        }
    }
}
