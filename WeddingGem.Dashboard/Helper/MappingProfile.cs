using AutoMapper;
using WeddingGem.API.DTOs;
using WeddingGem.API.DTOs.BiddingsDtos;
using WeddingGem.API.DTOs.Services;
using WeddingGem.Data.Entites;
using WeddingGem.Data.Entites.services;
using static System.Net.WebRequestMethods;

namespace WeddingGem.Dashboard.Helper
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



            CreateMap<Bidding, BidsViewOnly>()
                    .ForMember(b => b.Status, B => B.MapFrom(s => s.Status))
                    .ForMember(b => b.DateTime, s => s.MapFrom(s => s.DateTime.ToString("yyyy-MM-dd HH:MM")))
                    .ForMember(b => b.Needs, s => s.MapFrom(s => s.Services.Select(s => s.Name)));
        }

    }
}
