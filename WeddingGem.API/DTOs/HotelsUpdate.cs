using System.ComponentModel.DataAnnotations;

namespace WeddingGem.API.DTOs
{
    public class HotelsUpdate
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public string? imgName { get; set; }

        [Required(ErrorMessage = "IMG is required")]
        public IFormFile ImgUrl { get; set; }
    }
}
