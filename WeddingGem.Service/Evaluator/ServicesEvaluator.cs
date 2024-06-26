using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.API.DTOs.Services;
using WeddingGem.Data.Entites.services;
using WeddingGem.Repository.Interface;
using WeddingGem.Repository.Parames;
using WeddingGem.Repository.Specifications;
using WeddingGem.Service.DTOs;
using WeddingGem.Service.DTOS;

namespace WeddingGem.Service.Evaluator
{
    public class ServicesEvaluator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServicesEvaluator(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> GenerateServForBid()
        {

            #region specs
            var selectorWed = (Expression<Func<WeddingHall, WeddingHall>>)(p => new WeddingHall { Id = p.Id, Name = p.Name });

            var selectorCars = (Expression<Func<cars, cars>>)(p => new cars { Id = p.Id, Name = p.Name });

            var selectorHotel = (Expression<Func<Hotel, Hotel>>)(p => new Hotel { Id = p.Id, Name = p.Name });

            var selectorEnter = (Expression<Func<Entertainment, Entertainment>>)(p => new Entertainment { Id = p.Id, Name = p.Name });

            var selectorSelfCare = (Expression<Func<SelfCare, SelfCare>>)(p => new SelfCare { Id = p.Id, Name = p.Name });

            var selectorHoneyMoon = (Expression<Func<HoneyMoon, HoneyMoon>>)(p => new HoneyMoon { Id = p.Id, Name = p.Name });
            #endregion

            #region assigned specs
            var specswed = new ProductSpecification<WeddingHall>(selectorWed);

            var specscar = new ProductSpecification<cars>(selectorCars);

            var specshotel = new ProductSpecification<Hotel>(selectorHotel);

            var specseEnter = new ProductSpecification<Entertainment>(selectorEnter);

            var specsSelf = new ProductSpecification<SelfCare>(selectorSelfCare);

            var specsHoney = new ProductSpecification<HoneyMoon>(selectorHoneyMoon);
            #endregion

            #region data
            var Datawed = await _unitOfWork.Repository<WeddingHall>().GetAllAsyncWithSpec(specswed);

            var Datacar = await _unitOfWork.Repository<cars>().GetAllAsyncWithSpec(specscar);

            var Datahotel = await _unitOfWork.Repository<Hotel>().GetAllAsyncWithSpec(specshotel);

            var Dataenter = await _unitOfWork.Repository<Entertainment>().GetAllAsyncWithSpec(specseEnter);

            var Dataself = await _unitOfWork.Repository<SelfCare>().GetAllAsyncWithSpec(specsSelf);

            var Datahoney = await _unitOfWork.Repository<HoneyMoon>().GetAllAsyncWithSpec(specsHoney);
            #endregion

            #region allListsNeeded
            List<object> list = new List<object>();

            List<BaseBids> weds = Datawed.Select(item => new BaseBids
            {
                id = item.Id,
                ServiceName = item.Name
            }).ToList();

            GetBidsDetails allweds = new GetBidsDetails()
            {
                Category = "Wedding Halls",
                Services = weds,
            };

            list.Add(allweds);

            List<BaseBids> cars = Datacar.Select(item => new BaseBids
            {
                id = item.Id,
                ServiceName = item.Name
            }).ToList();

            GetBidsDetails allcars = new GetBidsDetails()
            {
                Category = "Cars",
                Services = cars,
            };

            list.Add(allcars);

            List<BaseBids> hotels = Datahotel.Select(item => new BaseBids
            {
                id = item.Id,
                ServiceName = item.Name
            }).ToList();

            GetBidsDetails allhotels = new GetBidsDetails()
            {
                Category = "Hotels",
                Services = hotels,
            };

            list.Add(allhotels);

            List<BaseBids> entertainments = Dataenter.Select(item => new BaseBids
            {
                id = item.Id,
                ServiceName = item.Name
            }).ToList();

            GetBidsDetails allenters = new GetBidsDetails()
            {
                Category = "Entertainemnts",
                Services = entertainments,
            };

            list.Add(allenters);

            List<BaseBids> selfcares = Dataself.Select(item => new BaseBids
            {
                id = item.Id,
                ServiceName = item.Name
            }).ToList();

            GetBidsDetails allself = new GetBidsDetails()
            {
                Category = "Self Cares",
                Services = selfcares,
            };

            list.Add(allself);

            List<BaseBids> honeys = Datahoney.Select(item => new BaseBids
            {
                id = item.Id,
                ServiceName = item.Name
            }).ToList();

            GetBidsDetails allhoneys = new GetBidsDetails()
            {
                Category = "honeymoons",
                Services = honeys,
            }; 
            #endregion

            list.Add(allhoneys);

            return list;
        } 
        public async Task<IEnumerable<object>> GenerateServForProducts()
        {

            #region data
            var Datawed = await _unitOfWork.Repository<WeddingHall>().GetAllAsync();

            var Datacar = await _unitOfWork.Repository<cars>().GetAllAsync();

            var Datahotel = await _unitOfWork.Repository<Hotel>().GetAllAsync();

            var Dataenter = await _unitOfWork.Repository<Entertainment>().GetAllAsync();

            var Dataself = await _unitOfWork.Repository<SelfCare>().GetAllAsync();

            var Datahoney = await _unitOfWork.Repository<HoneyMoon>().GetAllAsync();
            #endregion

            #region allListsNeeded
            List<object> list = new List<object>();

            var weds = _mapper.Map<IEnumerable<MainProduct>>(Datawed).ToList();

            ProductDto allweds = new ProductDto()
            {
                Category = "Wedding Halls",
                AllProduct = weds,
            };

            list.Add(allweds);

            var cars = _mapper.Map<IEnumerable<MainProduct>>(Datacar).ToList();

            ProductDto allcars = new ProductDto()
            {
                Category = "Cars",
                AllProduct = cars,
            };

            list.Add(allcars);

            var hotels = _mapper.Map<IEnumerable<MainProduct>>(Datahotel).ToList();

            ProductDto allhotels = new ProductDto()
            {
                Category = "Hotels",
                AllProduct = hotels,
            };

            list.Add(allhotels);

            var entertainments = _mapper.Map<IEnumerable<MainProduct>>(Dataenter).ToList();

            ProductDto allenters = new ProductDto()
            {
                Category = "Entertainemnts",
                AllProduct = entertainments,
            };

            list.Add(allenters);

            var selfcares = _mapper.Map<IEnumerable<MainProduct>>(Dataself).ToList();

            ProductDto allself = new ProductDto()
            {
                Category = "Self Cares",
                AllProduct = selfcares,
            };

            list.Add(allself);

            var honeys = _mapper.Map<IEnumerable<MainProduct>>(Datahoney).ToList();

            ProductDto allhoneys = new ProductDto()
            {
                Category = "honeymoons",
                AllProduct = honeys,
            }; 
            #endregion

            list.Add(allhoneys);

            return list;
        } 

    }
}
