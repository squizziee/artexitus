using Artexitus.IdentityMicroservice.Domain.Repositories;
using Artexitus.IdentityMicroservice.Contracts.DTO;
using Artexitus.IdentityMicroservice.Contracts.Requests.Queries.Roles;
using AutoMapper;
using MediatR;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Roles.Queries
{
    public class GetRolesHandler : IRequestHandler<GetRolesQuery, IEnumerable<RoleDTO>>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;

        public GetRolesHandler(IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDTO>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _userRoleRepository.GetAllAsync(cancellationToken);

            return roles.Select(_mapper.Map<RoleDTO>);
        }
    }
}
