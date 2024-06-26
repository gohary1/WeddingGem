using WeddingGem.Data.Entites.services;

namespace WeddingGem.API.DTOs
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public List<BaseProductDto> services { get; set; }
    }
}
