namespace WeddingGem.API.DTOs.BiddingsDtos
{
    public class GetBidsDetails
    {
        public string Category { get; set; }
        public List<BaseBids> Services { get; set; }
    }
}
