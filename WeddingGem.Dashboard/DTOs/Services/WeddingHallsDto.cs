using WeddingGem.Data.Entites;

namespace WeddingGem.API.DTOs.Services
{
    public class WeddingHallsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public string HallType { get; set; }
        public string? AvlDateFrom { get; set; }
        public string Description { get; set; }
        public decimal? Ratings { get; set; }
        public string ImgUrl { get; set; }

    }
}
