using AutoMapper;
using WeddingGem.API.DTOs;
using WeddingGem.API.DTOs.BiddingsDtos;
using WeddingGem.API.DTOs.Services;
using WeddingGem.Data.Entites;
using WeddingGem.Data.Entites.services;
using WeddingGem.Data.Service;
using WeddingGem.Service.DTOs;
using WeddingGem.Service.DTOS;
using static System.Net.WebRequestMethods;

namespace WeddingGem.API.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<WeddingHall, WeddingHallsDto>()
                .ForMember(o=>o.ImgUrl,l=>l.MapFrom<ProductPictureResolver<WeddingHall,WeddingHallsDto>>());

            CreateMap<cars, CarsDto>()
                .ForMember(o => o.ImgUrl, l => l.MapFrom<ProductPictureResolver<cars, CarsDto>>());

            CreateMap<HoneyMoon, HoneyMoonDto>()
                .ForMember(o => o.ImgUrl, l => l.MapFrom<ProductPictureResolver<HoneyMoon, HoneyMoonDto>>());

            CreateMap<Hotel, HotelDto>()
                .ForMember(o => o.ImgUrl, l => l.MapFrom<ProductPictureResolver<Hotel, HotelDto>>());

            CreateMap<Entertainment, EntertainmentDto>()
                .ForMember(o => o.ImgUrl, l => l.MapFrom<ProductPictureResolver<Entertainment, EntertainmentDto>>());

            CreateMap<SelfCare, SelfCareDto>()
                .ForMember(o => o.ImgUrl, l => l.MapFrom<ProductPictureResolver<SelfCare, SelfCareDto>>());




            CreateMap<WeddingHall, MainProduct>()
                .ForMember(o => o.ImgUrl, l => l.MapFrom<ProductPictureResolver<WeddingHall, MainProduct>>());

            CreateMap<cars, MainProduct>()
                .ForMember(o => o.ImgUrl, l => l.MapFrom<ProductPictureResolver<cars, MainProduct>>());

            CreateMap<HoneyMoon, MainProduct>()
                .ForMember(o => o.ImgUrl, l => l.MapFrom<ProductPictureResolver<HoneyMoon, MainProduct>>());

            CreateMap<Hotel, MainProduct>()
                .ForMember(o => o.ImgUrl, l => l.MapFrom<ProductPictureResolver<Hotel, MainProduct>>());

            CreateMap<Entertainment, MainProduct>()
                .ForMember(o => o.ImgUrl, l => l.MapFrom<ProductPictureResolver<Entertainment, MainProduct>>());

            CreateMap<SelfCare, MainProduct>()
                .ForMember(o => o.ImgUrl, l => l.MapFrom<ProductPictureResolver<SelfCare, MainProduct>>());


            CreateMap<CustomerBusket, CustomerBasketDto>()
            .ForMember(dest => dest.services, opt => opt.MapFrom(src => src.services)); 

            CreateMap<Items, BaseProductDto>()
                .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ratings, opt => opt.MapFrom(src => src.Ratings))
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.ImgUrl))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));




            //CreateMap<Bidding, BidsViewOnly>()
            //        .ForMember(b => b.Status, B => B.MapFrom(s => s.Status))
            //        .ForMember(b => b.DateTime, s => s.MapFrom(s => s.DateTime.ToString("yyyy-MM-dd HH:MM")))
            //        .ForMember(b => b.Needs, s => s.MapFrom(s => s.Services));

            CreateMap<Bidding, BidsView>()
            .ForMember(b => b.Status, opt => opt.MapFrom(s => s.Status)) 
            .ForMember(b => b.DateTime, opt => opt.MapFrom(s => s.DateTime.ToString("yyyy-MM-dd HH:mm"))) 
            .ForMember(b => b.Needs, opt => opt.MapFrom(s => s.Services));

            CreateMap<Items, BidProduct>()
                .ForMember(bp => bp.Name, opt => opt.MapFrom(i => i.Name))
                .ForMember(bp => bp.ImgUrl, opt => opt.MapFrom(i => i.ImgUrl));
        }

    }
}
