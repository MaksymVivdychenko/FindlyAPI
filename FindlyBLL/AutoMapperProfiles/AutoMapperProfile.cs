using AutoMapper;
using FindlyBLL.DTOs.CatalogDtos;
using FindlyBLL.DTOs.OffersDto;
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
                opt.MapFrom(src => src.Offers.Any() ? src.Offers.Min(q => q.Price) : 0))
            .ForMember(dest => dest.MaxPrice, opt =>
                opt.MapFrom(src => src.Offers.Any() ? src.Offers.Max(q => q.Price) : 0))
            .ForMember(dest => dest.IsAvailable, opt =>
            opt.MapFrom(src => src.Offers.Any(q => q.IsAvailable)));

        CreateMap<Offer, OfferDto>()
            .ForMember(dest => dest.ShopName, opt =>
                opt.MapFrom(src => src.Shop.Name))
            .ForMember(dest => dest.ShopLogoUrl, opt =>
                opt.MapFrom(src => src.Shop.ShopImageUrl));

        CreateMap<UserLikedOffers, LikedOfferDto>()
            .ForMember(dest => dest.BookTitle, opt =>
                opt.MapFrom(src => src.Offer.Book.Title))
            .ForMember(dest => dest.BookImageUrl, opt =>
                opt.MapFrom(src => src.Offer.Book.ImageUrl))
            .ForMember(dest => dest.Authors, opt =>
                opt.MapFrom(src => src.Offer.Book.Authors.Select(q => q.Name)))
            .ForMember(dest => dest.ShopName, opt =>
                opt.MapFrom(src => src.Offer.Shop.Name))
            .ForMember(dest => dest.Link, opt =>
                opt.MapFrom(src => src.Offer.Link))
            .ForMember(dest => dest.CurrentPrice, opt =>
                opt.MapFrom(src => src.Offer.Price))
            .ForMember(dest => dest.IsAvailable, opt => 
                opt.MapFrom(src => src.Offer.IsAvailable))
            .ForMember(dest => dest.IsNotifySet, opt =>
                opt.MapFrom(src => src.PriceToNotify != null));
    }
}