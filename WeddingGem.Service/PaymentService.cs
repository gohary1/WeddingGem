using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites;
using WeddingGem.Data.Entites.services;
using WeddingGem.Data.Service;
using WeddingGem.Repository.Interface;

namespace WeddingGem.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public PaymentService(IConfiguration configuration,IUnitOfWork unitOfWork,UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<CustomerBusket> CreateOrUpdateInetent(string userId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeKey:SecretKey"];
            var user=await _userManager.FindByIdAsync(userId);
            var productIds = await _userManager.Users
                                     .Include(u => u.UserServices)
                                     .ThenInclude(us => us.Service)
                                     .FirstOrDefaultAsync(u => u.Email == user.Email);
            var products=new List<Items>();
            foreach (var productId in productIds.UserServices)
            {
                var product = await _unitOfWork.Repository<Items>().GetAsync(productId.ServId);
                if (product != null)
                {
                    products.Add(product);
                }
            }
            var basket = new CustomerBusket()
            {
                Id = userId,
                services = products
            };
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)basket.services.Sum(p => p.Price * 100), // Stripe expects the amount in cents
                Currency = "usd",
                Metadata = new Dictionary<string, string>
                {
                    { "UserId", userId }
                }
            };

            var service = new PaymentIntentService();
            PaymentIntent intent;

            if (basket.PaymentIntentId == null)
            {
                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = options.Amount
                };
                intent = await service.UpdateAsync(basket.PaymentIntentId, updateOptions);
            }

            return basket;
        }
    }
}
