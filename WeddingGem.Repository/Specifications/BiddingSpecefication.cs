using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites;

namespace WeddingGem.Repository.Specifications
{
    public class BiddingSpecefication:BaseSpecification<Bidding> 
    {
        public BiddingSpecefication():base()
        {
            Includes.Add(p => p.Services);
            Includes.Add(p => p.AppUsers);
            Includes.Add(p => p.VendorBid);
        }
        public BiddingSpecefication(BiddingStatus status):base(p=>p.Status==status)
        {
            Includes.Add(p => p.Services);
            Includes.Add(p => p.AppUsers);
            Includes.Add(p => p.VendorBid);
        }
        public BiddingSpecefication(string id) : base(p => p.UserId ==id)
        {
            Includes.Add(p => p.Services);
            Includes.Add(p => p.AppUsers);
            Includes.Add(p => p.VendorBid);
        }
        public BiddingSpecefication(IEnumerable<int> ids):base(p=> ids.Contains(p.Id))
        {
            Includes.Add(p => p.Services);
            Includes.Add(p => p.AppUsers);
            Includes.Add(p => p.VendorBid);
        }
    }
}
