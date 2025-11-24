using AutoMapper;
using FindlyBLL.DTOs.UserDtos;
using FindlyDAL.Entities;

namespace FindlyBLL.AutoMapperProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<LoginUserDto, User>().ReverseMap();
        CreateMap<RegisterUserDto, User>().ReverseMap();
        
    }
}