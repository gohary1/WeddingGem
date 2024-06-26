using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using WeddingGem.API.DTOs.BiddingsDtos;
using WeddingGem.API.DTOs.Services;
using WeddingGem.API.Error;
using WeddingGem.API.Helper;
using WeddingGem.Data.Entites;
using WeddingGem.Data.Entites.services;
using WeddingGem.Repository.Interface;
using WeddingGem.Repository.Parames;
using WeddingGem.Repository.Params;
using WeddingGem.Repository.Specifications;
using WeddingGem.Service.Evaluator;

namespace WeddingGem.API.Controllers
{

    public class ProductController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ProductController(IUnitOfWork unitOfWork
                                ,IMapper mapper
                                )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region AllProducts
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<List<object>>> GetAllProducts()
        {
            var result=await new ServicesEvaluator(_unitOfWork,_mapper).GenerateServForProducts();
            return Ok(result);
        }
        #endregion

        #region WeddingHalls

        [HttpGet("GetAllHalls")]
        [ProducesResponseType(typeof(WeddingHallsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<WeddingHallsDto>>> getAllHalls([FromQuery]WeddingSpecsParams? specs=null)
        {
            var spec = new ProductSpecification<WeddingHall>(specs);
            var result = await _unitOfWork.Repository<WeddingHall>().GetAllAsyncWithSpec(spec);
            return Ok(_mapper.Map<IEnumerable<WeddingHallsDto>>(result));
        }

       

        [HttpGet("GetHall/{id}")]
        [ProducesResponseType(typeof(WeddingHallsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WeddingHall>> getHallById(int id)
        {
            var result = new ProductSpecification<WeddingHall>(id);
            var product= await _unitOfWork.Repository<WeddingHall>().GetAsyncWithSpec(result);
            if (product != null)
            {
                return Ok(_mapper.Map<WeddingHallsDto>(product));
            }
            else
            {
                return NotFound(new ApiResponse(404));
            }

        }

        #endregion

        #region Cars
        [HttpGet("GetAllCars")]
        [ProducesResponseType(typeof(CarsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CarsDto>>> getAllCars([FromQuery] BaseProductParams? specs = null)
        {
            var spec = new ProductSpecification<cars>(specs);
            var result = await _unitOfWork.Repository<cars>().GetAllAsyncWithSpec(spec);
            return Ok(_mapper.Map<IEnumerable<CarsDto>>(result));
        }


        [HttpGet("GetCar/{id}")]
        [ProducesResponseType(typeof(WeddingHallsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<cars>> getCarById(int id)
        {
            var result = new ProductSpecification<cars>(id);
            var product = await _unitOfWork.Repository<cars>().GetAsyncWithSpec(result);
            if (product != null)
            {
                return Ok(_mapper.Map<CarsDto>(product));
            }
            else
            {
                return NotFound(new ApiResponse(404));
            }
        }
        #endregion

        #region honeyMoon
        [HttpGet("GetAllHoneyMoons")]
        [ProducesResponseType(typeof(HoneyMoonDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<HoneyMoonDto>>> getAllHoneyMoon([FromQuery] HoneyMoonSepcsParam? specs = null)
        {
            var spec = new ProductSpecification<HoneyMoon>(specs);
            var result = await _unitOfWork.Repository<HoneyMoon>().GetAllAsyncWithSpec(spec);
            return Ok(_mapper.Map<IEnumerable<HoneyMoonDto>>(result));
        }


        [HttpGet("GetHoneyMoon/{id}")]
        [ProducesResponseType(typeof(HoneyMoonDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HoneyMoon>> getHoneyMoonById(int id)
        {
            var result = new ProductSpecification<HoneyMoon>(id);
            var product = await _unitOfWork.Repository<HoneyMoon>().GetAsyncWithSpec(result);
            if (product != null)
            {
                return Ok(_mapper.Map<HoneyMoonDto>(product));
            }
            else
            {
                return NotFound(new ApiResponse(404));
            }
        }
        #endregion

        #region hotel
        [HttpGet("GetAllHotels")]
        [ProducesResponseType(typeof(HotelDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Hotel>>> getAllHotels([FromQuery] BaseProductParams? specs = null)
        {
            var spec = new ProductSpecification<Hotel>(specs);
            var result = await _unitOfWork.Repository<Hotel>().GetAllAsyncWithSpec(spec);
            return Ok(_mapper.Map<IEnumerable<HotelDto>>(result));
        }


        [HttpGet("GetHotel/{id}")]
        [ProducesResponseType(typeof(HotelDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Hotel>> getHotelById(int id)
        {
            var result = new ProductSpecification<Hotel>(id);
            var product = await _unitOfWork.Repository<Hotel>().GetAsyncWithSpec(result);
            if (product != null)
            {
                return Ok(_mapper.Map< HotelDto>(product));
            }
            else
            {
                return NotFound(new ApiResponse(404));
            }
        }
        #endregion

        #region Entertainments
        [HttpGet("GetAllEntertainments")]
        [ProducesResponseType(typeof(EntertainmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<EntertainmentDto>>> getAllEntertainment([FromQuery] EntertainmentSpecsParam? specs = null)
        {
            var spec = new ProductSpecification<Entertainment>(specs);
            var result = await _unitOfWork.Repository<Entertainment>().GetAllAsyncWithSpec(spec);
            return Ok(_mapper.Map<IEnumerable<EntertainmentDto>>(result));
        }


        [HttpGet("GetEntertainment/{id}")]
        [ProducesResponseType(typeof(EntertainmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Entertainment>> getEntertainmentById(int id)
        {
            var result = new ProductSpecification<Entertainment>(id);
            var product = await _unitOfWork.Repository<Entertainment>().GetAsyncWithSpec(result);
            if (product != null)
            {
                return Ok(_mapper.Map< EntertainmentDto>(product));
            }
            else
            {
                return NotFound(new ApiResponse(404));
            }
        }
        #endregion

        #region SelfCare
        [HttpGet("GetAllSelfCare")]
        [ProducesResponseType(typeof(SelfCareDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<SelfCareDto>>> getAllSelfCare([FromQuery] SelfCareSpecsParams? specs = null)
        {
            var spec = new ProductSpecification<SelfCare>(specs);
            var result = await _unitOfWork.Repository<SelfCare>().GetAllAsyncWithSpec(spec);
            return Ok(_mapper.Map<IEnumerable<SelfCareDto>>(result));
        }


        [HttpGet("GetSelfCare/{id}")]
        [ProducesResponseType(typeof(SelfCareDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SelfCareDto>> getSelfCareById(int id)
        {
            var result = new ProductSpecification<SelfCare>(id);
            var product = await _unitOfWork.Repository<SelfCare>().GetAsyncWithSpec(result);
            if (product != null)
            {
                return Ok(_mapper.Map<SelfCareDto>(product));
            }
            else
            {
                return NotFound(new ApiResponse(404));
            }
        }
        #endregion


    }
}
