using WeddingGem.Service.DTOs;

namespace WeddingGem.API.DTOs.Services
{
    public class ProductDto
    {
        public string Category { get; set; }

        public List<MainProduct> AllProduct { get; set; }
    }
}
