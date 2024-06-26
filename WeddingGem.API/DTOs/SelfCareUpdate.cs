using System.ComponentModel.DataAnnotations;

namespace WeddingGem.API.DTOs
{
    public class SelfCareUpdate
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public string? imgName { get; set; }

        [Required(ErrorMessage = "IMG is required")]
        public IFormFile ImgUrl { get; set; }
    }
}
