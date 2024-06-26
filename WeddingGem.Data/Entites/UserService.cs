using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites.services;

namespace WeddingGem.Data.Entites
{
    public class UserService
    {
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        [ForeignKey("Service")]

        public int ServId { get; set; }
        public Items Service { get; set; }
        public DateTime purchaseDate { get; set; }= DateTime.Now;
    }
}
