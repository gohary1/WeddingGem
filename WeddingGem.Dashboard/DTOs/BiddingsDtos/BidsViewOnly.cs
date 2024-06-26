using System.ComponentModel.DataAnnotations.Schema;
using WeddingGem.Data.Entites.services;
using WeddingGem.Data.Entites;

namespace WeddingGem.API.DTOs.BiddingsDtos
{
    public class BidsViewOnly
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string DateTime { get; set; } 
        public ICollection<string> Needs { get; set; }
        public string? AcceptedBy { get; set; }
    }
}
