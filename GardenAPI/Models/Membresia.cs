namespace GardenAPI.Models
{
    public class Membresia
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
    }
}
