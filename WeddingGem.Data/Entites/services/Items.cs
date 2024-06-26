using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeddingGem.Data.Entites.services
{
    public class Items:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal? Ratings { get; set; }
        public string ImgUrl { get; set; }
        public ICollection<UserService> UserService { get; set; }

        public ICollection<Bidding> Biddings { get; set; }
        public string Vendor_Id { get; set; }
        public Vendor Vendor { get; set; }
    }
}
