using System.ComponentModel.DataAnnotations.Schema;
using WeddingGem.Data.Entites.services;
using WeddingGem.Data.Entites;
using System.ComponentModel.DataAnnotations;

namespace WeddingGem.API.DTOs.BiddingsDtos
{
    public class BiddingDto
    {
        [Required(ErrorMessage ="Please insert the price you want")]
        [Range(1000, double.MaxValue, ErrorMessage = "Number must be greater than 1000.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please insert the services you want")]
        public ICollection<int> ServicesID { get; set; }
    }
}
