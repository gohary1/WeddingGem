using AutoMapper;
using Demo_Dal.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Claims;
using WeddingGem.API.DTOs;
using WeddingGem.API.Error;
using WeddingGem.Data.Entites;
using WeddingGem.Data.Service;
using WeddingGem.Repository.Interface;

namespace WeddingGem.API.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PaymentController(IPaymentService paymentService,IUnitOfWork unitOfWork,UserManager<AppUser> userManager,IMapper mapper,IConfiguration configuration)
        {
            _paymentService = paymentService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("CreatePayment")]
        public async Task<ActionResult<CustomerBusket>> CreateOrUpdatePaymentIntern()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(buyerEmail);
            if (user == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var result = await _paymentService.CreateOrUpdateInetent(user.Id);
            var map = _mapper.Map<CustomerBasketDto>(result);
            foreach (var service in map.services)
            {
                service.ImgUrl = $"{_configuration["ApiBaseUrl"]}/{service.ImgUrl}";
            }
            return Ok(map);
        }
    }
}
