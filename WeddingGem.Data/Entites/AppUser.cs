using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WeddingGem.Data.Entites.services;

namespace WeddingGem.Data.Entites
{
    public class AppUser:IdentityUser
    {
        [MaxLength(30)]
        public string? Address { get; set; }
        public string? BirthDate { get; set; }
        public ICollection<UserService> UserServices { get; set; }
        public ICollection<Bidding> Biddings { get; set; }
    }
}
