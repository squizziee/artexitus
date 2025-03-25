using Artexitus.IdentityMicroservice.Contracts.DTO;
using Artexitus.IdentityMicroservice.Domain.Entities;
using AutoMapper;

namespace Artexitus.IdentityMicroservice.Application.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email)
                )
                .ForMember(
                    dest => dest.Username,
                    opt => opt.MapFrom(src => src.Profile.Username)
                )
                .ForMember(
                    dest => dest.Role,
                    opt => opt.MapFrom(src => src.Profile.Role.Name)
                )
                .ForMember(
                    dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => src.CreatedAt)
                );
        }
    }
}
