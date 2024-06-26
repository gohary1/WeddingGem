using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WeddingGem.API.DTOs;
using WeddingGem.API.Error;
using WeddingGem.Data.Entites;
using WeddingGem.Data.Entites.services;
using WeddingGem.Repository.Interface;
using WeddingGem.Repository.Specifications;

namespace WeddingGem.API.Controllers
{

    public class AddProductController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public AddProductController(IUnitOfWork unitOfWork,IMapper mapper,UserManager<AppUser> userManager,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("AddProduct/{id}")]
        public async Task<ActionResult> AddProduct(int id)
        {
            if (id != null)
            {
                var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(buyerEmail);

                var spec = new BaseSpecification<UserService>(e => e.UserId == user.Id);
                var userServices = await _unitOfWork.Repository<UserService>().GetAllAsyncWithSpec(spec);
                foreach (var userServ in userServices)
                {
                    if (userServ.ServId == id)
                    {
                        return BadRequest(new ApiExceptionRes(401) {Details="Failed as it was added already"});
                    }
                }
                var specs = new BaseSpecification<Items>(e => e.Id == id);
                var product = await _unitOfWork.Repository<Items>().GetAsyncWithSpec(specs);
                if (product != null)
                {
                    
                    UserService addItem = new UserService()
                    {
                        UserId = user.Id,
                        ServId = product.Id,
                        purchaseDate = DateTime.Now,
                    };
                    var result = await _unitOfWork.Repository<UserService>().AddAsync(addItem);
                    if (result > 0)
                    {
                        return Ok(new Succsess() { message = "Product Added Successfuly" });
                    }
                }
                return NotFound(new ApiResponse(404));
            }
            return BadRequest(new ApiResponse(401));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("DeleteProduct/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if (id != null)
            {
                var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(buyerEmail);
                var specs = new BaseSpecification<UserService>(e => e.UserId == user.Id);
                var product = await _unitOfWork.Repository<UserService>().GetAllAsyncWithSpec(specs);
                if (product != null)
                {
                    foreach ( var spec in product) 
                    {
                        if (spec.ServId == id)
                        {
                            await _unitOfWork.Repository<UserService>().DeleteAsync(spec);
                            await _unitOfWork.CompleteAsync();
                            return Ok(new Succsess() { message = "Product Deleted Successfuly" });
                        }
                    }
                }    
                return NotFound(new ApiResponse(404));
            }
            return BadRequest(new ApiResponse(401));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllPurchased")]
        public async Task<ActionResult> GetAllPurchased()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(buyerEmail);

            var spec = new BaseSpecification<UserService>(e => e.UserId == user.Id);
            var userServices = await _unitOfWork.Repository<UserService>().GetAllAsyncWithSpec(spec);
            if(userServices != null && userServices.Count()!=0)
            {
            List<BaseProductDto> baseProduct = new List<BaseProductDto>();

                foreach (var serv in userServices)
                {
                    var specs = new BaseSpecification<Items>(e=>e.Id==serv.ServId);
                    var services = await _unitOfWork.Repository<Items>().GetAsyncWithSpec(specs);
                    BaseProductDto product = new BaseProductDto()
                    {
                        id=services.Id,
                        Name=services.Name,
                        ImgUrl=$"{_configuration["ApiBaseUrl"]}/{services.ImgUrl}",
                        price=services.Price,
                        ratings=services.Ratings
                    };
                    baseProduct.Add(product);   
                }
                PurchasedProductsDto purchased = new PurchasedProductsDto()
                {
                    Name = user.UserName,
                    Products = baseProduct
                };
                return Ok(purchased);
            }
            return NotFound(new ApiExceptionRes(404) { Details="There is not products yet added"});
            
        }

    }
}
