using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users;
using MediatR;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Users
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user == null)
            {
                throw new ResourceDoesNotExistException($"No user with id {request.Id} was found. Unable to deactivate");
            }

            await _userRepository.DeleteAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
