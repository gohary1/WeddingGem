namespace WeddingGem.API.DTOs
{
    public class BaseProductDto
    {
        public int? id { get; set; }
        public string Name { get; set; }
        public decimal price { get; set; }
        public decimal? ratings { get; set; }
        public string ImgUrl { get; set; }
    }
}
