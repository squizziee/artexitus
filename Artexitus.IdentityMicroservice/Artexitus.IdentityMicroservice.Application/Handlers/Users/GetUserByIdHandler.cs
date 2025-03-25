using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Contracts.DTO;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Queries.User;
using AutoMapper;
using MediatR;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Users
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user == null)
            {
                throw new ResourceDoesNotExistException($"User with id {request.Id} does not exist. Unable to fetch");
            }

            return _mapper.Map<UserDTO>(user);
        }
    }
}
