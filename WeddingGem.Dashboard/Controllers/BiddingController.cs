using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.NetworkInformation;
using System.Security.Claims;
using WeddingGem.API.DTOs.BiddingsDtos;
using WeddingGem.Data.Entites;
using WeddingGem.Data.Entites.services;
using WeddingGem.Repository.Interface;
using WeddingGem.Repository.Specifications;

namespace WeddingGem.Dashboard.Controllers
{
    public class BiddingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public BiddingController(IUnitOfWork unitOfWork,IMapper mapper,UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<IActionResult> PendingBids()
        {
            var status=BiddingStatus.Pending;
            var specs = new BiddingSpecefication(status);
            var bids = await _unitOfWork.Repository<Bidding>().GetAllAsyncWithSpec(specs);
            return View(_mapper.Map<IEnumerable<Bidding>,IEnumerable<BidsViewOnly>>(bids));
        }
        [Authorize]
        public async Task<IActionResult> AcceptBid(int id)
        {
            var emaill=User.FindFirstValue(ClaimTypes.Email);
            var user=await _userManager.FindByEmailAsync(emaill);
            if (user!=null)
            {
                VendorBid vendorBid = new VendorBid()
                {
                    VendorId=user.Id,
                    AcceptedBid_Id=id,
                    purchaseDate=DateTime.Now
                };
                var result=await _unitOfWork.Repository<VendorBid>().AddAsync(vendorBid);
                if(result > 0)
                {
                    var bidItem=await _unitOfWork.Repository<Bidding>().GetAsync(id);
                    bidItem.Status=BiddingStatus.Accepted;
                    await _unitOfWork.Repository<Bidding>().UpdateAsync(bidItem);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction("AcceptedBids");
                }
            }
            return RedirectToAction("AcceptedBids");
        }
        [Authorize]
        public async Task<IActionResult> AcceptedBids()
        {

            if (User.IsInRole("Admin"))
            {
                var status=BiddingStatus.Accepted;
                var specs = new BiddingSpecefication(status);
                var bids = await _unitOfWork.Repository<Bidding>().GetAllAsyncWithSpec(specs);
                var vendorIds = bids.SelectMany(e => e.VendorBid.Select(v => v.VendorId)).Distinct();
                var vendors = new List<string>();
                foreach (var vendorId in vendorIds)
                {
                    var vendor = await _userManager.FindByIdAsync(vendorId);
                    if (vendor != null)
                    {
                        vendors.Add(vendor.UserName);
                    }
                }
                var result = _mapper.Map<IEnumerable<Bidding>, IEnumerable<BidsViewOnly>>(bids);
                for(var i = 0; i < result.Count(); i++)
                {
                    result.ElementAt(i).AcceptedBy = vendors.ElementAtOrDefault(i);
                }
                return View(result);
            }

                var emaill = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(emaill);
            if (user != null)
            {
                var specs = new VendorBidSpecification(user.Id);
                var vendorBids = await _unitOfWork.Repository<VendorBid>().GetAllAsyncWithSpec(specs);

                var ids = vendorBids.Select(p => p.AcceptedBid_Id);

                var spec = new BiddingSpecefication(ids);
                var bids = await _unitOfWork.Repository<Bidding>().GetAllAsyncWithSpec(spec);

                return View(_mapper.Map<IEnumerable<Bidding>, IEnumerable<BidsViewOnly>>(bids));

            }
            return View();
            
        }
        
        public async Task<IActionResult> BidDelete(int id)
        {
            var specs =new VendorBidSpecification(id);
            var bid=await _unitOfWork.Repository<Bidding>().GetAsync(id);
            var bidVendor=await _unitOfWork.Repository<VendorBid>().GetAsyncWithSpec(specs);
            if (bid != null)
            {
                var delted = _unitOfWork.Repository<VendorBid>().DeleteAsync(bidVendor);
                var deltedd = _unitOfWork.Repository<Bidding>().DeleteAsync(bid);
                var result = await _unitOfWork.CompleteAsync();
                return RedirectToAction("PendingBids");
            }
            return RedirectToAction("PendingBids");
        }
    }
}
