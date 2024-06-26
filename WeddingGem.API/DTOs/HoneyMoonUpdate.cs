using System.ComponentModel.DataAnnotations;

namespace WeddingGem.API.DTOs
{
    public class HoneyMoonUpdate
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public string? imgName { get; set; }

        [Required(ErrorMessage = "IMG is required")]
        public IFormFile ImgUrl { get; set; }

        [Required(ErrorMessage = "Destination is required")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "StartDate is required")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is required")]
        public string EndDate { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Inclusions { get; set; }
    }
}
