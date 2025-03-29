using Artexitus.IdentityMicroservice.Domain.Repositories;
using Artexitus.IdentityMicroservice.Contracts.DTO;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Queries.Roles;
using AutoMapper;
using MediatR;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Roles.Queries
{
    public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, RoleDTO>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;

        public GetRoleByIdHandler(IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<RoleDTO> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _userRoleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (role == null)
            {
                throw new ResourceDoesNotExistException($"Role with id {request.Id} does not exist. Unable to fetch");
            }

            return _mapper.Map<RoleDTO>(role);
        }
    }
}
