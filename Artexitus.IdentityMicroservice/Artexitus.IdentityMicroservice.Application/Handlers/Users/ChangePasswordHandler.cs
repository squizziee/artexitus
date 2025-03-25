using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users;
using MediatR;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Users
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
