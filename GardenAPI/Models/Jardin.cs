namespace GardenAPI.Models
{
    public class Jardin
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public int UserID { get; set; }
        public int SensorPackID { get; set; }

        public Usuario? User { get; set; }
        public SensorPack? SensorPack { get; set; }
    }
}
