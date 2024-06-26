using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites;

namespace WeddingGem.Repository.Specifications
{
    public class VendorBidSpecification:BaseSpecification<VendorBid>
    {
        public VendorBidSpecification(string id):base(p=>p.VendorId==id)
        {
            
        }
        public VendorBidSpecification(int id):base(p=>p.AcceptedBid_Id==id)
        {
            
        }
    }
}
