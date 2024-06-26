using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingGem.Dashboard.Models;
using WeddingGem.Data.Entites;
using WeddingGem.Repository.Interface;

namespace WeddingGem.Dashboard.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users=await _userManager.Users.ToListAsync();
            var mappedUser=users.Select(u=> new UserViewModel()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Roles= _userManager.GetRolesAsync(u).Result,
            }).ToList();
            return View(mappedUser);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var allroles = await _roleManager.Roles.ToListAsync();
            var pack = await _unitOfWork.Repository<Packages>().GetAllAsync();
            
            if(await _userManager.IsInRoleAsync(user, "Vendor"))
            {
                var vend = await _unitOfWork.Repository<Vendor>().GetAsync(id);
                var planNameAssigen = "";
                foreach (var item in pack) { if (item.Id == vend.PackageId) { planNameAssigen = item.PlanName; } }
                var userRoless = new UserRoleView()
                {
                    id = user.Id,
                    UserName = user.UserName,
                    PlanName=planNameAssigen,
                    Roles = allroles.Select(r => new RoleViewModel
                    {
                        id = r.Id,
                        Name = r.Name,
                        IsSelected = _userManager.IsInRoleAsync(user, r.Name).Result
                    }).ToList(),
                    Packages = pack.ToList()
                };
                return View(userRoless);
            }
            var userRoles = new UserRoleView()
            {
                id = user.Id,
                UserName = user.UserName,
                Roles = allroles.Select(r => new RoleViewModel
                {
                    id = r.Id,
                    Name = r.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList(),
                Packages=pack.ToList()
            };
            return View(userRoles);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleView model)
        {
            
                var user = await _userManager.FindByIdAsync(model.id);
                if (user == null)
                {
                    return NotFound();
                }
                var UserRoles = await _userManager.GetRolesAsync(user);
                foreach(var role in model.Roles)
                {
                    if (UserRoles.Any(r => r == role.Name) && !role.IsSelected)
                        await _userManager.RemoveFromRoleAsync(user, role.Name);
                    if (!UserRoles.Any(r => r == role.Name) && role.IsSelected)
                        await _userManager.AddToRoleAsync(user, role.Name);     
                }
                if (await _userManager.IsInRoleAsync(user, "Vendor"))
                {
                    var vendor = await _unitOfWork.Repository<Vendor>().GetAsync(user.Id);
                    var allpacks = await _unitOfWork.Repository<Packages>().GetAllAsync();
                if (vendor != null)
                {
                    var selectedPackage = allpacks.FirstOrDefault(p => p.PlanName == model.PlanName);
                    if (selectedPackage != null)
                    {
                        vendor.PackageId = selectedPackage.Id;
                        await _unitOfWork.Repository<Vendor>().UpdateAsync(vendor);
                        await _unitOfWork.CompleteAsync();
                    }
                }
                else
                {
                    int id = 0;
                    foreach (var pack in allpacks) { if (pack.PlanName == model.PlanName) { id = pack.Id; } }
                    Vendor vendorr = new Vendor()
                    {
                        Id = user.Id,
                        PackageId = id
                    };
                    await _unitOfWork.Repository<Vendor>().AddAsync(vendorr);
                    await _unitOfWork.CompleteAsync();
                }
            }
            return RedirectToAction("Index");
            
        }
        [HttpGet]
        public async Task<IActionResult> create(CreateUserModel model)
        {
            if (TempData["ErrorMessage"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["ErrorMessage"].ToString());
            }
            var roles = await _roleManager.Roles.ToListAsync();
            var packages = await _unitOfWork.Repository<Packages>().GetAllAsync();
            var user = new CreateUserModel()
            {
                AllPacks = packages.ToList(),
                AllRoles = roles.Select(r => new RoleFormViewDeleteModel()
                {
                    Name = r.Name
                }).ToList()
            };
         
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserModel model)
        {
            if (!ModelState.IsValid)
            {
                var roles = await _roleManager.Roles.ToListAsync();
                model.AllRoles = roles.Select(r => new RoleFormViewDeleteModel
                {
                    id = r.Id,
                    Name = r.Name
                }).ToList();

                return View(model);
            }
            if (!model.RoleName.Any(e => e == "Vendor") || model.packName != null)
            {
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    TempData["ErrorMessage"] = "Email already exists";
                    return RedirectToAction("create", model);
                }

                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    BirthDate = model.Birthday,
                    Address = model.Address,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, model.RoleName);
                    foreach(var role in model.RoleName)
                    {
                        if (role == "Vendor")
                        {
                            var allpacks = await _unitOfWork.Repository<Packages>().GetAllAsync();
                            int id=0;
                            foreach (var pack in allpacks) { if (pack.PlanName == model.packName) { id = pack.Id; } }
                            Vendor vendor = new Vendor()
                            {
                                Id = user.Id,
                                PackageId=id
                            };
                            await _unitOfWork.Repository<Vendor>().AddAsync(vendor);
                            await _unitOfWork.CompleteAsync();
                        }
                    }
                    return RedirectToAction("Index","User");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                // hatle kol elroles tany 3shan nemla elmodel
                var roles = await _roleManager.Roles.ToListAsync();
                var packages = await _unitOfWork.Repository<Packages>().GetAllAsync();
                var users = new CreateUserModel()
                {
                    AllPacks = packages.ToList(),
                    AllRoles = roles.Select(r => new RoleFormViewDeleteModel()
                    {
                        Name = r.Name
                    }).ToList()
                };

                return View(users);
            }
            else
            {
                ModelState.AddModelError("", "Please select Plan for the Vendor");
                return RedirectToAction("create",model);
            }
            
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user =await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result=await _userManager.DeleteAsync(user);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}

