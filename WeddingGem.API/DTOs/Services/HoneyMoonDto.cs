using WeddingGem.Data.Entites;

namespace WeddingGem.API.DTOs.Services
{
    public class HoneyMoonDto:BaseProductDto
    {

        public string Description { get; set; }
        public string Destination { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Inclusions { get; set; }
    }
}
