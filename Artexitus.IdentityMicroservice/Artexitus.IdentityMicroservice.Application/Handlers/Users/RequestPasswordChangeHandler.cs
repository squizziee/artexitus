using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Application.Services;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Users
{
    public class RequestPasswordChangeHandler : IRequestHandler<RequestPasswordChangeCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheAccessor _cacheAccessor;
        private readonly IEmailService _emailService;
        private readonly ILogger<RequestPasswordChangeHandler> _logger;

        public RequestPasswordChangeHandler(IUserRepository userRepository, 
            ICacheAccessor cacheAccessor,
            IEmailService emailService,
            ILogger<RequestPasswordChangeHandler> logger)
        {
            _userRepository = userRepository;
            _cacheAccessor = cacheAccessor;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task Handle(RequestPasswordChangeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user == null)
            {
                throw new ResourceDoesNotExistException($"No user with email {request.Email} was found. Unable to request password reset");
            }

            var resetId = GenerateId();

            _cacheAccessor.Set(
                $"pw_reset_{resetId}", 
                new PasswordChangeRequest
                {
                    UserId = user.Id
                }, 
                TimeSpan.FromMinutes(15)
            );

            BackgroundJob.Enqueue(
                () => _emailService.SendPasswordResetEmail(user, resetId, cancellationToken)
            );

            _logger.LogInformation("Added password reset request for user with email {email}. 15 minutes to proceed", user.Email);
        }

        private static string GenerateId()
        {
            return Guid.NewGuid().ToString().Replace('-', '0');   
        }
    }
}
