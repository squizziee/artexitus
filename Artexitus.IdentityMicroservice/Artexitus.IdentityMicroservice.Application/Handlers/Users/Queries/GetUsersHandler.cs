using Artexitus.IdentityMicroservice.Application.ConfigurationSections;
using Artexitus.IdentityMicroservice.Domain.Repositories;
using Artexitus.IdentityMicroservice.Contracts.DTO;
using Artexitus.IdentityMicroservice.Contracts.Requests.Queries.User;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;

namespace Artexitus.IdentityMicroservice.Application.Handlers.Users.Queries
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, PaginatedResponse<UserDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly PaginationSettings _paginationSettings;

        public GetUsersHandler(IUserRepository userRepository,
            IMapper mapper, IOptions<PaginationSettings> options)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _paginationSettings = options.Value;
        }

        public async Task<PaginatedResponse<UserDTO>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository
                .GetPaginatedAsync(request.PageNumber, _paginationSettings.UsersPageSize, cancellationToken);

            return new PaginatedResponse<UserDTO>
            {
                Items = users.Items.Select(_mapper.Map<UserDTO>),
                PageNumber = users.PageNumber,
                TotalPages = users.TotalPages,
            };
        }
    }
}
