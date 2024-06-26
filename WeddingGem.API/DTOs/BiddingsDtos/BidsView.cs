using WeddingGem.API.DTOs.Services;

namespace WeddingGem.API.DTOs.BiddingsDtos
{
    public class BidsView
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string DateTime { get; set; }
        public ICollection<BidProduct> Needs { get; set; }
    }
}
