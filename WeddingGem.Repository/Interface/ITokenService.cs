using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites;

namespace WeddingGem.Repository.Interface
{
    public interface ITokenService
    {
        public Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager);
    }
}
