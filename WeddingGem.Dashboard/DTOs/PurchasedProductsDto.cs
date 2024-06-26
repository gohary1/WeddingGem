namespace WeddingGem.API.DTOs
{
    public class PurchasedProductsDto
    {
        public string Name { get; set; }

        public List<BaseProductDto> Products { get; set; }
    }
}
