using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.Json;
using WeddingGem.API.DTOs.BiddingsDtos;
using WeddingGem.API.Error;
using WeddingGem.Data.Entites;
using WeddingGem.Data.Entites.services;
using WeddingGem.Repository.Interface;
using WeddingGem.Repository.Specifications;
using WeddingGem.Service.Evaluator;

namespace WeddingGem.API.Controllers
{

    public class BiddingController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public BiddingController(IUnitOfWork unitOfWork
                                ,IMapper mapper
                                ,UserManager<AppUser> userManager
                                ,IConfiguration configuration
                                )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }


        [HttpGet("GetBiddingDetails")]
        public async Task<ActionResult<List<object>>> GetBidsDetails()
        {
            var repository = await new ServicesEvaluator(_unitOfWork, _mapper).GenerateServForBid();          
            return Ok(repository);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("InsertBid")]
        public async Task<ActionResult<Bidding>> InsertBid(BiddingDto model)
        {
            if (model != null)
            {
                var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
                var user=await _userManager.FindByEmailAsync(buyerEmail);
                List<Items> services = new List<Items>();
                foreach (var service in model.ServicesID)
                {
                    var newItem = await _unitOfWork.Repository<Items>().GetAsync(service);
                    if (newItem != null)
                    {
                        services.Add(newItem);
                    }
                    else
                    {
                        return NotFound(new ApiResponse(404));
                    }
                }
                Bidding newBid = new Bidding()
                {
                    Price = model.Price,
                    DateTime = DateTime.Now,
                    Status = BiddingStatus.Pending,
                    UserId = user.Id,
                    Services = services
                };
                var result = await _unitOfWork.Repository<Bidding>().AddAsync(newBid);
                if (result > 0)
                {
                    return RedirectToAction("GetAllBids");
                }
            }
            return BadRequest(new ApiExceptionRes(400) { Details = "fill the form please" });

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllBids")]
        public async Task<ActionResult<List<BidsViewOnly>>> getAllBids()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(buyerEmail);
            var biddings = new BiddingSpecefication(user.Id);
            var bid = await _unitOfWork.Repository<Bidding>().GetAllAsyncWithSpec(biddings);
            if (bid != null)
            {
                var all = _mapper.Map<IEnumerable<Bidding>, IEnumerable<BidsView>>(bid).ToList();
                foreach (var bidding in all)
                {
                    foreach (var  b in bidding.Needs) 
                    {
                        b.ImgUrl = $"{_configuration["ApiBaseUrl"]}/{b.ImgUrl}";
                    }
                }
                return Ok(all);
            }
            return BadRequest();
        }
    }
}
