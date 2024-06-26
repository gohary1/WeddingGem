namespace WeddingGem.API.DTOs
{
    public class BaseProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? Ratings { get; set; }
        public string ImgUrl { get; set; }
    }
}
