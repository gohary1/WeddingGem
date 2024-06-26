using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites.services;

namespace WeddingGem.Data.Entites
{
    public class Vendor
    {
        [ForeignKey("AppUser")]
        public string Id { get; set; }
        public AppUser AppUser { get; set; }

        [ForeignKey("Packages")]
        public int PackageId { get; set; }
        public Packages Packages { get; set; }

        public ICollection<VendorBid> Bidding { get; set; }
        public ICollection<Items> Service { get; set; }
    }
}
