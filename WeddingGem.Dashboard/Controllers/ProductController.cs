using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Drawing;
using System.Security.Claims;
using WeddingGem.API.DTOs;
using WeddingGem.API.DTOs.Services;
using WeddingGem.Dashboard.Helper;
using WeddingGem.Dashboard.Service;
using WeddingGem.Data.Entites;
using WeddingGem.Data.Entites.services;
using WeddingGem.Repository.Interface;
using WeddingGem.Repository.Params;
using WeddingGem.Repository.Repository;
using WeddingGem.Repository.Specifications;
using WeddingGem.Service.Evaluator;

namespace WeddingGem.Dashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IFileUploadService _fileUploadService;

        public ProductController(IUnitOfWork unitOfWork
                                ,IMapper mapper
                                ,UserManager<AppUser> userManager
                                ,IConfiguration configuration
                                ,IFileUploadService FileUploadService
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _fileUploadService = FileUploadService;
        }
        #region weddings
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> WeddingIndex()
        {

            if (User.IsInRole("Admin"))
            {
                var spec = new ProductSpecification<WeddingHall>();
                var result = await _unitOfWork.Repository<WeddingHall>().GetAllAsyncWithSpec(spec);
                return View("Views/Product/weds/WeddingIndex.cshtml", _mapper.Map< IEnumerable<WeddingHallsDto>>(result));
            }
            else
            {
                var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(buyerEmail);
                var spec = new ProductSpecification<WeddingHall>(p=>p.Vendor_Id==user.Id);
                var result = await _unitOfWork.Repository<WeddingHall>().GetAllAsyncWithSpec(spec);
                return View("Views/Product/weds/WeddingIndex.cshtml",_mapper.Map< IEnumerable<WeddingHallsDto>>(result));
            }

             
        }
        [HttpGet]
        public async Task<IActionResult> WeddingUpdate(int id)
        {
            var model = await _unitOfWork.Repository<WeddingHall>().GetAsync(id);
            WeddingHallUpdate hall = new WeddingHallUpdate()
            {
                id = model.Id,
                AvlDateFrom = model.AvlDateFrom,
                Capacity = model.Capacity,
                Description = model.Description,
                HallType = model.HallType,
                Location = model.Location,
                imgName = $"{_configuration["ApiBaseUrl"]}/{model.ImgUrl}",
                Name = model.Name,
                Price = model.Price
            };
            return View("Views/Product/weds/WeddingUpdate.cshtml", hall);
        }

        public async Task<IActionResult> WeddingUpdate(WeddingHallUpdate HallModel)
        {

            var hall = await _unitOfWork.Repository<WeddingHall>().GetAsync(HallModel.id);
            if (hall != null)
            {
                hall.Name = HallModel.Name ?? hall.Name;
                hall.AvlDateFrom = HallModel.AvlDateFrom ?? hall.AvlDateFrom;
                hall.HallType = HallModel.HallType ?? hall.HallType;
                if (HallModel.Capacity != null) { hall.Capacity = HallModel.Capacity; }
                if(HallModel.ImgUrl!=null)
                {
                    DocumentSettings.DeleteFile(hall.ImgUrl);
                    string imgName = DocumentSettings.UploadFile(HallModel.ImgUrl, "WeddingHalls");
                    hall.ImgUrl= imgName;
                }
                
                hall.Location = HallModel.Location ?? hall.Location;
                hall.Description = HallModel.Description ?? hall.Description;
                if (HallModel.Price != null) { hall.Price = HallModel.Price; }
                var result = _unitOfWork.Repository<WeddingHall>().UpdateAsync(hall);
                var final = await _unitOfWork.CompleteAsync();
                if (result.IsCompleted)
                {
                    return RedirectToAction("WeddingIndex");
                }
                return View("Views/Product/weds/WeddingUpdate.cshtml", HallModel);
            }

            return View("Views/Product/weds/WeddingUpdate.cshtml", HallModel);
        }
        [HttpGet]
        public async Task<IActionResult> createHall()
        {
            return View("Views/Product/weds/create.cshtml");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> createHall(WeddingHallUpdate HallModel)
        {
            if (ModelState.IsValid)
            {
                var id = User.FindFirstValue(ClaimTypes.Email);
                var vendor=await _userManager.FindByEmailAsync(id);
                var IMG = DocumentSettings.UploadFile(HallModel.ImgUrl, "WeddingHalls");
                WeddingHall hall = new WeddingHall()
                {
                    Name= HallModel.Name,
                    AvlDateFrom = HallModel.AvlDateFrom,
                    Capacity= HallModel.Capacity,
                    Description= HallModel.Description,
                    Price= HallModel.Price,
                    HallType= HallModel.HallType,
                    ImgUrl= IMG,
                    Location= HallModel.Location,
                    Ratings=4.7M,
                    Vendor_Id=vendor.Id,
                };
                var result = await _unitOfWork.Repository<WeddingHall>().AddAsync(hall);
                await _unitOfWork.CompleteAsync();
                if (result > 0)
                {
                    return RedirectToAction("WeddingIndex");
                }
            }
            return View("Views/Product/weds/create.cshtml",HallModel);
        }
        public async Task<IActionResult> WeddingDelete(int id)
        {
            var specs = new ProductSpecification<WeddingHall>(id);
            var res = await _unitOfWork.Repository<WeddingHall>().GetAsyncWithSpec(specs);

            if (res.Biddings.Count() == 0)
            {
                DocumentSettings.DeleteFile(res.ImgUrl);
                await _unitOfWork.Repository<WeddingHall>().DeleteAsync(res);  // Ensure async operation is awaited
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("WeddingIndex");
                }

                TempData["delete"] = "This service is already in a bid. You cannot delete it.";
                return RedirectToAction("WeddingIndex");


         }
        #endregion#region weddings

        #region hotels
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> HotelsIndex()
        {
            if (User.IsInRole("Admin"))
            {
                var spec = new ProductSpecification<Hotel>();
                var result = await _unitOfWork.Repository<Hotel>().GetAllAsyncWithSpec(spec);
                return View("Views/Product/Hotels/HotelsIndex.cshtml", _mapper.Map< IEnumerable<HotelDto>>(result));
            }
            else
            {
                var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(buyerEmail);

                var spec = new ProductSpecification<Hotel>(p => p.Vendor_Id == user.Id);
                var result = await _unitOfWork.Repository<Hotel>().GetAllAsyncWithSpec(spec);
                return View("Views/Product/Hotels/HotelsIndex.cshtml", _mapper.Map<IEnumerable<HotelDto>>(result));
            }
        }
        [HttpGet]
        public async Task<IActionResult> hotelUpdate(int id)
        {
            var model = await _unitOfWork.Repository<Hotel>().GetAsync(id);
            HotelsUpdate hotel = new HotelsUpdate()
            {
                id = model.Id,
                Description = model.Description,
                Location = model.Location,
                imgName = $"{_configuration["ApiBaseUrl"]}/{model.ImgUrl}",
                Name = model.Name,
                Price = model.Price
            };
            return View("Views/Product/Hotels/HotelsUpdate.cshtml", hotel);
        }

        public async Task<IActionResult> hotelUpdate(HotelsUpdate hotelModel)
        {

            var hotel = await _unitOfWork.Repository<Hotel>().GetAsync(hotelModel.id);
            if (hotel != null)
            {
                hotel.Name = hotelModel.Name ?? hotel.Name;
                hotel.Location = hotelModel.Location ?? hotel.Location;
                hotel.Description = hotelModel.Description ?? hotel.Description;
                if (hotelModel.Price != null) { hotel.Price = hotelModel.Price; }
                if (hotelModel.ImgUrl != null)
                {
                    DocumentSettings.DeleteFile(hotel.ImgUrl);
                    string imgName = DocumentSettings.UploadFile(hotelModel.ImgUrl, "Hotels");
                    hotel.ImgUrl = imgName;
                }
                var result = _unitOfWork.Repository<Hotel>().UpdateAsync(hotel);
                var final = await _unitOfWork.CompleteAsync();
                if (result.IsCompleted)
                {
                    return RedirectToAction("HotelsIndex");
                }
                return View("Views/Product/Hotels/HotelsUpdate.cshtml", hotelModel);
            }

            return View("Views/Product/Hotels/HotelsUpdate.cshtml", hotelModel);
        }

        [HttpGet]
        public async Task<IActionResult> createHotel()
        {
            return View("Views/Product/Hotels/create.cshtml");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> createHotel(HotelsUpdate hotelModel)
        {
            if (ModelState.IsValid)
            {
                var id = User.FindFirstValue(ClaimTypes.Email);
                var vendor = await _userManager.FindByEmailAsync(id);
                var IMG = DocumentSettings.UploadFile(hotelModel.ImgUrl, "Hotels");
                Hotel hotel = new Hotel()
                {
                    Name = hotelModel.Name,
                    Description = hotelModel.Description,
                    Price = hotelModel.Price,
                    ImgUrl = IMG,
                    Location = hotelModel.Location,
                    Ratings = 4.7M,
                    Vendor_Id = vendor.Id,
                };
                var result = await _unitOfWork.Repository<Hotel>().AddAsync(hotel);
                await _unitOfWork.CompleteAsync();
                if (result > 0)
                {
                    return RedirectToAction("HotelsIndex");
                }
            }
            return View("Views/Product/Hotels/create.cshtml", hotelModel);
        }
        public async Task<IActionResult> HotelDelete(int id)
        {
            var specs = new ProductSpecification<Hotel>(id);
            var res = await _unitOfWork.Repository<Hotel>().GetAsyncWithSpec(specs);

            if (res.Biddings.Count() == 0)
            {
                DocumentSettings.DeleteFile(res.ImgUrl);
                await _unitOfWork.Repository<Hotel>().DeleteAsync(res);  // Ensure async operation is awaited
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("HotelsIndex");
            }

            TempData["delete"] = "This service is already in a bid. You cannot delete it.";
            return RedirectToAction("HotelsIndex");
        }
        #endregion

        #region Entertainments
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EntertainmentsIndex()
        {
            if (User.IsInRole("Admin"))
            {
                var spec = new ProductSpecification<Entertainment>();
                var result = await _unitOfWork.Repository<Entertainment>().GetAllAsyncWithSpec(spec);
                return View("Views/Product/Entertainment/EntertainmentsIndex.cshtml", _mapper.Map<IEnumerable<EntertainmentDto>>(result));
            }
            else
            {
                var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(buyerEmail);

                var spec = new ProductSpecification<Entertainment>(p => p.Vendor_Id == user.Id);
                var result = await _unitOfWork.Repository<Entertainment>().GetAllAsyncWithSpec(spec);
                return View("Views/Product/Entertainment/EntertainmentsIndex.cshtml", _mapper.Map<IEnumerable<EntertainmentDto>>(result));

            }
        }
        [HttpGet]
        public async Task<IActionResult> EnetrtainmentUpdate(int id)
        {
            var model = await _unitOfWork.Repository<Entertainment>().GetAsync(id);
            EntertainmentUpdate entertainment = new EntertainmentUpdate()
            {
                id = model.Id,
                Description = model.Description,
                imgName = $"{_configuration["ApiBaseUrl"]}/{model.ImgUrl}",
                BandType = model.TypeBand,
                Name = model.Name,
                Price = model.Price
            };
            return View("Views/Product/Entertainment/EntertainmentUpdate.cshtml", entertainment);
        }

        public async Task<IActionResult> EntertainmentUpdate(EntertainmentUpdate EntertainmentModel)
        {
  
                var entertainment = await _unitOfWork.Repository<Entertainment>().GetAsync(EntertainmentModel.id);
                if (entertainment != null)
                     {
                        entertainment.Name = EntertainmentModel.Name ?? entertainment.Name;
                        if (EntertainmentModel.ImgUrl != null)
                        {
                            DocumentSettings.DeleteFile(entertainment.ImgUrl);
                            string imgName = DocumentSettings.UploadFile(EntertainmentModel.ImgUrl, "Entertainment");
                            entertainment.ImgUrl = imgName;
                        }
                        entertainment.TypeBand = EntertainmentModel.BandType ?? entertainment.TypeBand;
                        entertainment.Description = EntertainmentModel.Description ?? entertainment.Description;
                        if (EntertainmentModel.Price != null) { entertainment.Price = EntertainmentModel.Price; }

                        var result = _unitOfWork.Repository<Entertainment>().UpdateAsync(entertainment);
                        var final = await _unitOfWork.CompleteAsync();

                        if (result.IsCompleted)
                        {
                            return RedirectToAction("EntertainmentsIndex");
                        }
                        return View("Views/Product/Entertainment/EntertainmentUpdate.cshtml", EntertainmentModel);
                    }
            return View("Views/Product/Entertainment/EntertainmentUpdate.cshtml", EntertainmentModel);

        }

        [HttpGet]
        public async Task<IActionResult> createEntertainment()
        {
            return View("Views/Product/Entertainment/create.cshtml");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> createEntertainment(EntertainmentUpdate EntertainmentModel)
        {
            if (ModelState.IsValid)
            {
                var id = User.FindFirstValue(ClaimTypes.Email);
                var vendor = await _userManager.FindByEmailAsync(id);
                var IMG = DocumentSettings.UploadFile(EntertainmentModel.ImgUrl, "Entertainment");
                Entertainment entertainment = new Entertainment()
                {
                    Name = EntertainmentModel.Name,
                    TypeBand = EntertainmentModel.BandType,
                    Description = EntertainmentModel.Description,
                    Price = EntertainmentModel.Price,
                    ImgUrl = IMG,
                    Ratings = 4.7M,
                    Vendor_Id = vendor.Id,
                };
                var result = await _unitOfWork.Repository<Entertainment>().AddAsync(entertainment);
                await _unitOfWork.CompleteAsync();
                if (result > 0)
                {
                    return RedirectToAction("EntertainmentsIndex");
                }
            }
            return View("Views/Product/Entertainment/create.cshtml", EntertainmentModel);
        }

        public async Task<IActionResult> EntertainmentDelete(int id)
        {
            var specs = new ProductSpecification<Entertainment>(id);
            var res = await _unitOfWork.Repository<Entertainment>().GetAsyncWithSpec(specs);

            if (res.Biddings.Count() == 0)
            {
                DocumentSettings.DeleteFile(res.ImgUrl);
                await _unitOfWork.Repository<Entertainment>().DeleteAsync(res);  // Ensure async operation is awaited
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("EntertainmentsIndex");
            }

            TempData["delete"] = "This service is already in a bid. You cannot delete it.";
            return RedirectToAction("EntertainmentsIndex");
        }
        #endregion

        #region Cars
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CarsIndex()
        {
            if (User.IsInRole("Admin"))
            {
                var spec = new ProductSpecification<cars>();
                var result = await _unitOfWork.Repository<cars>().GetAllAsyncWithSpec(spec);
                return View("Views/Product/Cars/CarsIndex.cshtml", _mapper.Map<IEnumerable<CarsDto>>(result));
            }
            else
            {
                var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(buyerEmail);

                var spec = new ProductSpecification<cars>(p => p.Vendor_Id == user.Id);
                var result = await _unitOfWork.Repository<cars>().GetAllAsyncWithSpec(spec);
                return View("Views/Product/Cars/CarsIndex.cshtml", _mapper.Map<IEnumerable<CarsDto>>(result));
            }
        }
        [HttpGet]
        public async Task<IActionResult> CarUpdate(int id)
        {
            var model = await _unitOfWork.Repository<cars>().GetAsync(id);
            CarsUpdate Car = new CarsUpdate()
            {
                id = model.Id,
                Description = model.Description,
                imgName = $"{_configuration["ApiBaseUrl"]}/{model.ImgUrl}",
                Color = model.Color,
                Model = model.Model,
                Name = model.Name,
                Price = model.Price,
                Capacity = model.Capacity

            };
            return View("Views/Product/Cars/CarUpdate.cshtml", Car);
        }
        [HttpPost]
        public async Task<IActionResult> CarUpdate(CarsUpdate cars)
        {

            var car = await _unitOfWork.Repository<cars>().GetAsync(cars.id);
            if (car != null)
            {
                car.Name = cars.Name ?? car.Name;
                if (cars.ImgUrl != null)
                {
                    DocumentSettings.DeleteFile(car.ImgUrl);
                    string imgName = DocumentSettings.UploadFile(cars.ImgUrl, "Cars");
                    car.ImgUrl = imgName;
                }
                car.Color = cars.Color ?? car.Color;
                car.Description = cars.Description ?? car.Description;
                car.Model = cars.Model ?? car.Model;
                if (cars.Price != null) { car.Price = cars.Price; }
                if (cars.Capacity != null) { car.Capacity = cars.Capacity; }

                var result = _unitOfWork.Repository<cars>().UpdateAsync(car);
                var final = await _unitOfWork.CompleteAsync();

                if (result.IsCompleted)
                {
                    return RedirectToAction("CarsIndex");
                }
                return View("Views/Product/Cars/CarsUpdate.cshtml", cars);
            }
            return View("Views/Product/Cars/CarsUpdate.cshtml", cars);

        }

        [HttpGet]
        public async Task<IActionResult> createCarr()
        {
            return View("Views/Product/Cars/create.cshtml");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> createCarr(CarsUpdate carModel)
        {
            if (ModelState.IsValid)
            {
                var id = User.FindFirstValue(ClaimTypes.Email);
                var vendor = await _userManager.FindByEmailAsync(id);
                var IMG = DocumentSettings.UploadFile(carModel.ImgUrl, "Cars");
                cars car = new cars()
                {
                    Name = carModel.Name,
                    Color= carModel.Color,
                    Capacity = carModel.Capacity,
                    Description = carModel.Description,
                    Price = carModel.Price,
                    Model = carModel.Model,
                    ImgUrl = IMG,
                    Ratings = 4.7M,
                    Vendor_Id = vendor.Id,
                };
                var result = await _unitOfWork.Repository<cars>().AddAsync(car);
                await _unitOfWork.CompleteAsync();
                if (result > 0)
                {
                    return RedirectToAction("CarsIndex");
                }
            }
            return View("Views/Product/Cars/create.cshtml", carModel);
        }

        public async Task<IActionResult> CarDelete(int id)
        {
            var specs = new ProductSpecification<cars>(id);
            var res = await _unitOfWork.Repository<cars>().GetAsyncWithSpec(specs);

            if (res.Biddings.Count() == 0)
            {
                DocumentSettings.DeleteFile(res.ImgUrl);
                await _unitOfWork.Repository<cars>().DeleteAsync(res);  // Ensure async operation is awaited
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("CarsIndex");
            }

            TempData["delete"] = "This service is already in a bid. You cannot delete it.";
            return RedirectToAction("CarsIndex");
        }
        #endregion

        #region SelfCare
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SelfIndex()
        {
            if (User.IsInRole("Admin"))
            {
                var spec = new ProductSpecification<SelfCare>();
                var result = await _unitOfWork.Repository<SelfCare>().GetAllAsyncWithSpec(spec);
                return View("Views/Product/SelfCare/SelfIndex.cshtml", _mapper.Map<IEnumerable<SelfCareDto>>(result));
            }
            else
            {
                var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(buyerEmail);

                var spec = new ProductSpecification<SelfCare>(p => p.Vendor_Id == user.Id);
                var result = await _unitOfWork.Repository<SelfCare>().GetAllAsyncWithSpec(spec);
                return View("Views/Product/SelfCare/SelfIndex.cshtml", _mapper.Map<IEnumerable<SelfCareDto>>(result));
            }
        }
        [HttpGet]
        public async Task<IActionResult> SelfCareUpdate(int id)
        {
            var model = await _unitOfWork.Repository<SelfCare>().GetAsync(id);
            SelfCareUpdate selfCare = new SelfCareUpdate()
            {
                id = model.Id,
                Description = model.Description,
                imgName = $"{_configuration["ApiBaseUrl"]}/{model.ImgUrl}",
                Type = model.Type,
                Name = model.Name,
                Price = model.Price
            };
            return View("Views/Product/SelfCare/SelfCareUpdate.cshtml", selfCare);
        }

        public async Task<IActionResult> SelfCareUpdate(SelfCareUpdate selfCareObject)
        {

            var selfcare = await _unitOfWork.Repository<SelfCare>().GetAsync(selfCareObject.id);
            if (selfcare != null)
            {
                selfcare.Name = selfCareObject.Name ?? selfcare.Name;
                if (selfCareObject.ImgUrl != null)
                {
                    DocumentSettings.DeleteFile(selfcare.ImgUrl);
                    string imgName = DocumentSettings.UploadFile(selfCareObject.ImgUrl, "selfCare");
                    selfcare.ImgUrl = imgName;
                }
                selfcare.Type = selfCareObject.Type ?? selfcare.Type;
                selfcare.Description = selfCareObject.Description ?? selfcare.Description;
                if (selfCareObject.Price != null) { selfcare.Price = selfCareObject.Price; }

                var result = _unitOfWork.Repository<SelfCare>().UpdateAsync(selfcare);
                var final = await _unitOfWork.CompleteAsync();

                if (result.IsCompleted)
                {
                    return RedirectToAction("SelfIndex");
                }
                return View("Views/Product/SelfCare/SelfCareUpdate.cshtml", selfCareObject);
            }
            return View("Views/Product/SelfCare/SelfCareUpdate.cshtml", selfCareObject);

        }

        [HttpGet]
        public async Task<IActionResult> createSelf()
        {
            return View("Views/Product/SelfCare/create.cshtml");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> createSelf(SelfCareUpdate selfModel)
        {
            if (ModelState.IsValid)
            {
                var id = User.FindFirstValue(ClaimTypes.Email);
                var vendor = await _userManager.FindByEmailAsync(id);
                var IMG = DocumentSettings.UploadFile(selfModel.ImgUrl, "selfCare");
                SelfCare self = new SelfCare()
                {
                    Name = selfModel.Name,
                    Type = selfModel.Type,
                    Description = selfModel.Description,
                    Price = selfModel.Price,
                    ImgUrl = IMG,
                    Ratings = 4.7M,
                    Vendor_Id = vendor.Id,
                };
                var result = await _unitOfWork.Repository<SelfCare>().AddAsync(self);
                await _unitOfWork.CompleteAsync();
                if (result > 0)
                {
                    return RedirectToAction("SelfIndex");
                }
            }
            return View("Views/Product/SelfCare/create.cshtml", selfModel);
        }

        public async Task<IActionResult> SelfDelete(int id)
        {
            var specs = new ProductSpecification<SelfCare>(id);
            var res = await _unitOfWork.Repository<SelfCare>().GetAsyncWithSpec(specs);

            if (res.Biddings.Count() == 0)
            {
                DocumentSettings.DeleteFile(res.ImgUrl);
                await _unitOfWork.Repository<SelfCare>().DeleteAsync(res);  // Ensure async operation is awaited
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("SelfIndex");
            }

            TempData["delete"] = "This service is already in a bid. You cannot delete it.";
            return RedirectToAction("SelfIndex");
        }
        #endregion

        #region honeyMoon
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> HoneyIndex()
        {
            if (User.IsInRole("Admin"))
            {
                var spec = new ProductSpecification<HoneyMoon>();
                var result = await _unitOfWork.Repository<HoneyMoon>().GetAllAsyncWithSpec(spec);
                return View("Views/Product/honey/HoneyIndex.cshtml", _mapper.Map<IEnumerable<HoneyMoonDto>>(result));
            }
            else
            {
                var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(buyerEmail);

                var spec = new ProductSpecification<HoneyMoon>(p => p.Vendor_Id == user.Id);
                var result = await _unitOfWork.Repository<HoneyMoon>().GetAllAsyncWithSpec(spec);
                return View("Views/Product/honey/HoneyIndex.cshtml", _mapper.Map<IEnumerable<HoneyMoonDto>>(result));
            }
        }
        [HttpGet]
        public async Task<IActionResult> HoneyUpdate(int id)
        {
            var model = await _unitOfWork.Repository<HoneyMoon>().GetAsync(id);
            HoneyMoonUpdate honeymoon = new HoneyMoonUpdate()
            {
                id = model.Id,
                Description = model.Description,
                imgName = $"{_configuration["ApiBaseUrl"]}/{model.ImgUrl}",
                Destination = model.Destination,
                Name = model.Name,
                Price = model.Price,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Inclusions = model.Inclusions
            };
            return View("Views/Product/honey/HoneyUpdate.cshtml", honeymoon);
        }

        public async Task<IActionResult> HoneyUpdate(HoneyMoonUpdate honeymoonObject)
        {

            var honeyMoon = await _unitOfWork.Repository<HoneyMoon>().GetAsync(honeymoonObject.id);
            if (honeyMoon != null)
            {
                honeyMoon.Name = honeymoonObject.Name ?? honeyMoon.Name;
                if (honeymoonObject.ImgUrl != null)
                {
                    DocumentSettings.DeleteFile(honeyMoon.ImgUrl);
                    string imgName = DocumentSettings.UploadFile(honeymoonObject.ImgUrl, "Honeymoon");
                    honeyMoon.ImgUrl = imgName;
                }
                honeyMoon.Destination = honeymoonObject.Destination ?? honeyMoon.Destination;
                honeyMoon.Description = honeymoonObject.Description ?? honeyMoon.Description;
                honeyMoon.StartDate = honeymoonObject.StartDate ?? honeyMoon.StartDate;
                honeyMoon.EndDate = honeymoonObject.EndDate ?? honeyMoon.EndDate;
                honeyMoon.Inclusions = honeymoonObject.Inclusions ?? honeyMoon.Inclusions;
                if (honeymoonObject.Price != null) { honeyMoon.Price = honeymoonObject.Price; }

                var result = _unitOfWork.Repository<HoneyMoon>().UpdateAsync(honeyMoon);
                var final = await _unitOfWork.CompleteAsync();

                if (result.IsCompleted)
                {
                    return RedirectToAction("HoneyIndex");
                }
                return View("Views/Product/honey/HoneyUpdate.cshtml", honeymoonObject);
            }
            return View("Views/Product/honey/HoneyUpdate.cshtml", honeymoonObject);

        }

        [HttpGet]
        public async Task<IActionResult> createHoney()
        {
            return View("Views/Product/honey/create.cshtml");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> createHoney(HoneyMoonUpdate honeyModel)
        {
            if (ModelState.IsValid)
            {
                var id = User.FindFirstValue(ClaimTypes.Email);
                var vendor = await _userManager.FindByEmailAsync(id);
                var IMG = DocumentSettings.UploadFile(honeyModel.ImgUrl, "Honeymoon");
                HoneyMoon honey = new HoneyMoon()
                {
                    Name = honeyModel.Name,
                    Destination = honeyModel.Destination,
                    Inclusions = honeyModel.Inclusions,
                    Description = honeyModel.Description,
                    Price = honeyModel.Price,
                    EndDate = honeyModel.EndDate,
                    ImgUrl = IMG,
                    StartDate = honeyModel.StartDate,
                    Ratings = 4.7M,
                    Vendor_Id = vendor.Id,
                };
                var result = await _unitOfWork.Repository<HoneyMoon>().AddAsync(honey);
                await _unitOfWork.CompleteAsync();
                if (result > 0)
                {
                    return RedirectToAction("HoneyIndex");
                }
            }
            return View("Views/Product/honey/create.cshtml", honeyModel);
        }

        public async Task<IActionResult> HoneyDelete(int id)
        {
            var specs = new ProductSpecification<HoneyMoon>(id);
            var res = await _unitOfWork.Repository<HoneyMoon>().GetAsyncWithSpec(specs);

            if (res.Biddings.Count() == 0)
            {
                DocumentSettings.DeleteFile(res.ImgUrl);
                await _unitOfWork.Repository<HoneyMoon>().DeleteAsync(res);  // Ensure async operation is awaited
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("HoneyIndex");
            }

            TempData["delete"] = "This service is already in a bid. You cannot delete it.";
            return RedirectToAction("HoneyIndex");
        }
        #endregion


    }
}
