using Artexitus.IdentityMicroservice.Contracts.DTO;
using Artexitus.IdentityMicroservice.Domain.Entities;
using AutoMapper;

namespace Artexitus.IdentityMicroservice.Application.Mappers
{
    public class RoleMapperProfile : Profile
    {
        public RoleMapperProfile()
        {
            CreateMap<UserRole, RoleDTO>();
        }
    }
}
