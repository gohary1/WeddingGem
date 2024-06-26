namespace WeddingGem.API.DTOs.Services
{
    public class CarsDto:BaseProductDto
    {
        public int Capacity { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
    }
}
