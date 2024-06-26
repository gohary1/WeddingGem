using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites;
using WeddingGem.Repository.Interface;

namespace WeddingGem.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,user.UserName),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var token = new JwtSecurityToken(issuer: _configuration["JWT:ValidIssuer"]
                , audience: _configuration["JWT:ValidAudience"],
                  expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:durationInDays"]))
                  , claims: authClaims
                  , signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                  );

            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
    }
}
