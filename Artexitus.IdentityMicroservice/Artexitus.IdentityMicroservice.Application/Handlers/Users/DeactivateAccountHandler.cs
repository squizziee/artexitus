using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users;
using MediatR;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Users
{
    public class DeactivateAccountHandler : IRequestHandler<DeactivateAccountCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeactivateAccountHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DeactivateAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user == null)
            {
                throw new ResourceDoesNotExistException($"No user with id {request.Id} was found. Unable to deactivate");
            }

            await _userRepository.SoftDeleteAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
