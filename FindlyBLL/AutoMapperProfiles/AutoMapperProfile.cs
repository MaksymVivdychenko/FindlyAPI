using AutoMapper;
using FindlyBLL.DTOs.CatalogDtos;
using FindlyBLL.DTOs.UserDtos;
using FindlyDAL.Entities;

namespace FindlyBLL.AutoMapperProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<LoginUserDto, User>();
        CreateMap<RegisterUserDto, User>();
        
        CreateMap<Cover, CoverGetDto>().ReverseMap();
        CreateMap<Publisher, PublisherGetDto>();

        CreateMap<Book, BookGetDto>()
            .ForMember(dest => dest.Authors, opt =>
                opt.MapFrom(src => src.Authors.Select(q => q.Name)))
            .ForMember(dest => dest.Cover, opt =>
                opt.MapFrom(src => src.Cover.Name))
            .ForMember(dest => dest.Publisher, opt =>
                opt.MapFrom(src => src.Publisher.Title))
            .ForMember(dest => dest.MinPrice, opt =>
                opt.MapFrom(src => src.Offers.Min(q => q.Price)))
            .ForMember(dest => dest.MaxPrice, opt =>
                opt.MapFrom(src => src.Offers.Max(q => q.Price)))
            .ForMember(dest => dest.IsAvailable, opt =>
            opt.MapFrom(src => src.Offers.Any(q => q.IsAvailable)));
    }
}