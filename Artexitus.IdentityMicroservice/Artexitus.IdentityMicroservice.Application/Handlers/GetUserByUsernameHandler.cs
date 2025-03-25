using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Contracts.DTO;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Artexitus.IdentityMicroservice.Application.Handlers
{
    public class GetUserByUsernameHandler : IRequestHandler<GetUserByUsernameQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByUsernameHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken);

            if (user == null)
            {
                throw new ResourceDoesNotExistException($"User with username {request.Username} does not exist. Unable to log in");
            }

            return _mapper.Map<UserDTO>(user);
        }
    }
}
