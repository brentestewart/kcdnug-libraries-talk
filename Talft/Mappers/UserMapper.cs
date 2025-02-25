using AutoMapper;
using Talft.Dtos;
using Talft.Models;

namespace Talft.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName));
    }
}
