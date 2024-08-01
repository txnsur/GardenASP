namespace GardenAPI.Models
{
    public class Plantilla
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int IdealLight { get; set; }
        public int IdealTemperature { get; set; }
        public int IdealMoisture { get; set; }
    }
}
