using System.ComponentModel.DataAnnotations;

namespace WeddingGem.API.DTOs
{
    public class CarsUpdate
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        public int? Capacity { get; set; }
        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }
        public string? Color { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public string? imgName { get; set; }

        [Required(ErrorMessage = "IMG is required")]
        public IFormFile ImgUrl { get; set; }

    }
}
