using Demo_Dal.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.VisualBasic;
using System.Web;
using WeddingGem.API.DTOs;
using WeddingGem.API.Error;
using WeddingGem.API.Helper;
using WeddingGem.Data.Entites;
using WeddingGem.Repository.Interface;

namespace WeddingGem.API.Controllers
{
    [Route("WeddingGem/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMailServices _mailServices;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMailServices mailServices
            ,IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mailServices = mailServices;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var email=await _userManager.FindByEmailAsync(model.Email);
            if (email == null) 
            { 
                return Unauthorized(new ApiExceptionRes(401) { Details="Email or Password is incorrect "});
            }
            var result = await _signInManager.CheckPasswordSignInAsync(email, model.Password, false);
            var result2 = await _userManager.IsInRoleAsync(email, "User");
            if (!result.Succeeded||!result2) { return Unauthorized(new ApiExceptionRes(401) { Details = "Email or Password is incorrect " }); };
  
            return Ok(new UserDto()
            {
                DisplayName=email.UserName,
                Email=email.Email,
                Token=await _tokenService.CreateToken(email, _userManager)
            });

        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (model != null)
            {
                var flag = await _userManager.FindByEmailAsync(model.Email);
                if (flag != null)
                {
                    return BadRequest(new ApiExceptionRes(400) { Details = "This email already taken try another one" }) ;
                }
                var user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    BirthDate=model.Birthday,
                    Address=model.Address,
                };
                var result=await _userManager.CreateAsync(user,model.Password);
                var result2=await _userManager.AddToRoleAsync(user,"User");
                if (result.Succeeded)
                {
                    var Email = new Email
                    {
                        To = model.Email,
                        Subject = "Password Reseted Successfuly!",
                        Body = $"Dear {user.UserName},\r\n\r\nCongratulations and welcome to the WeddingGem family! We are delighted to have you join us as you embark on your exciting journey to plan the perfect wedding.\r\n\r\nHere are the details of your registration:\r\n- **Username:** {user.UserName}\r\n- **Registration Date:** {DateTime.Now.ToString("MMMM dd, yyyy")} \r\n\r\nAt WeddingGem, we are dedicated to making your wedding planning experience as smooth and enjoyable as possible. As a valued member, you now have access to a variety of exclusive features:\r\n- **Personalized Planning Tools:** Tailor your wedding plans to suit your unique style and preferences.\r\n- **Top Vendors and Venues:** Explore and connect with the best vendors and venues in the industry.\r\n- **Custom Checklists and Timelines:** Stay organized with our detailed checklists and timelines.\r\n- **Inspiration Galleries:** Discover endless inspiration from real weddings and creative ideas.\r\n- **Expert Advice:** Benefit from tips and advice from seasoned wedding professionals.\r\n\r\nTo begin your journey,\r\n\r\nIf you have any questions or need any assistance, our support team is always here to help. Feel free to contact us at abdelrahmanelgohary3@gmail.com or 01155353197.\r\n\r\nThank you for choosing WeddingGem. We are honored to be a part of your special journey and look forward to helping you create unforgettable memories.\r\n\r\nWarmest regards,"
                    };
                    _mailServices.SendEmail(Email);
                    return Ok(new UserDto()
                    {
                        Email=model.Email,
                        DisplayName=model.UserName,
                        Token = await _tokenService.CreateToken(user, _userManager)
                    });
                }
                else { return Unauthorized(new ApiExceptionRes(401) { Details = "inputs is incorrect " }); };
            }
            { return Unauthorized(new ApiExceptionRes(401) { Details = "please fill the inputs" }); };
        }

        [HttpPost("ForgotPasswordEmail")]
        public async Task<ActionResult> ForgotPasswordEmail(ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound(new ApiExceptionRes(404) { Details = "User not found" });
            }


            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Construct password reset link
            //var resetLink = $"{Request.Scheme}://{Request.Host}/WeddingGem/Account/reset-password?email={model.Email}&token={HttpUtility.UrlEncode(token)}";
            var resetLink = $"{_configuration["BaseSendEmail"]}/reset-password?email={model.Email}&token={HttpUtility.UrlEncode(token)}";

            // Send password reset email
            var email = new Email
            {
                To = model.Email,
                Subject = "Reset Your Password",
                Body = $"Dear {user.UserName},\r\n\r\nWe received a request to reset your password for your WeddingGem account. Please click the link below to reset your password:\r\n\r\n{resetLink}\r\n\r\nIf you did not request a password reset, please ignore this email or contact support if you have any questions.\r\n\r\nThank you,\r\nWeddingGem Support Team\r\n\r\n---\r\n\r\nThis is an automated message, please do not reply."
            };
            _mailServices.SendEmail(email);

            return Ok(new { message = "Password reset instructions sent to your email" });
        }


        [HttpGet("reset-password")]
        public async Task<ActionResult> VerifyResetToken(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            var isValidToken = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
            if (!isValidToken)
            {
                return BadRequest(new { message = "Invalid token" });
            }

            return Ok(new { Message= token });
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                var email = new Email
                {
                    To = model.Email,
                    Subject = "Password Reseted Successfuly!",
                    Body = $"Dear {user.UserName},\r\n\r\nWe wanted to inform you that your password has been successfully reset. If you did not initiate this change, please contact our WeddingGem support team immediately.\r\n\r\nHere are the details of the reset:\r\n- **Date: {DateTime.Now.ToString("MMMM,dd,yyyy")}\r\n- **Time: {DateTime.Now.ToString("hh,mm,tt")}\r\n\r\nTo ensure the security of your account, we recommend the following:\r\n- Do not share your password with anyone.\r\n- Use a strong and unique password.\r\n- Enable two-factor authentication if you have not already done so.\r\n\r\nIf you have any questions or encounter any issues, please do not hesitate to reach out to our support team at WeddingGem0@gmail.com or 01155353197.\r\n\r\nThank you for your attention to this matter."
                };
                _mailServices.SendEmail(email);
                
                return Ok(new { message = "Password has been reset successfully" });
            }

            return BadRequest(result.Errors);
        }


    }
}
