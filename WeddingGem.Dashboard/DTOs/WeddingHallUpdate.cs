using System.ComponentModel.DataAnnotations;

namespace WeddingGem.API.DTOs
{
    public class WeddingHallUpdate
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [RegularExpression("^\\d+$", ErrorMessage = "Enter a valid number")]
        public int Capacity { get; set; }
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Halltype is required")]
        public string HallType { get; set; }
        public string? AvlDateFrom { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public string? imgName { get; set; }

        [Required(ErrorMessage = "IMG is required")]
        public IFormFile ImgUrl { get; set; }
    }
}
