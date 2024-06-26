using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingGem.Data.Entites
{
    public class VendorBid
    {
        [ForeignKey("Vendor")]
        public string VendorId { get; set; }
        public Vendor Vendor { get; set; }

        [ForeignKey("Bidding")]
        public int AcceptedBid_Id { get; set; }
        public Bidding Bidding { get; set; }

        public DateTime purchaseDate { get; set; } = DateTime.Now;
    }
}
