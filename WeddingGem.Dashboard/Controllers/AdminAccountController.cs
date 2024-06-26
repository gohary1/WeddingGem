using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WeddingGem.Dashboard.Models;
using WeddingGem.Data.Entites;
using WeddingGem.Repository.Interface;
using WeddingGem.Repository.Specifications;

namespace WeddingGem.Dashboard.Controllers
{
    public class AdminAccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public AdminAccountController(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager,ITokenService tokenService,IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded && await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        var token = await _tokenService.CreateToken(user, _userManager);
                        if (!string.IsNullOrEmpty(token))
                        {
                            // Store the token in session
                            HttpContext.Session.SetString("JWToken", token);
                            var storedToken = HttpContext.Session.GetString("JWToken");
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else if(result.Succeeded && await _userManager.IsInRoleAsync(user, "Vendor"))
                    {
                        var token = await _tokenService.CreateToken(user, _userManager);
                        if (!string.IsNullOrEmpty(token))
                        {
                            // Store the token in session
                            HttpContext.Session.SetString("JWToken", token);
                            var storedToken = HttpContext.Session.GetString("JWToken");
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else 
                    {
                        ModelState.AddModelError("password", "Password is incorrect");
                        return View(model);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("JWToken");

            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}
