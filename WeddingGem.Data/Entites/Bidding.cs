using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WeddingGem.Data.Entites.services;

namespace WeddingGem.Data.Entites
{
    public class Bidding:BaseEntity
    {
        public decimal Price { get; set; }

        public BiddingStatus Status { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        [ForeignKey("AppUsers")]

        public string UserId { get; set; }

        public AppUser AppUsers { get; set; }

        public ICollection<Items> Services { get; set; }

        public ICollection<VendorBid> VendorBid { get; set; }
    }
}
