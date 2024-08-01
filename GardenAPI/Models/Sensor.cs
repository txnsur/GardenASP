namespace GardenAPI.Models
{
    public class Sensor
    {
        public int ID { get; set; }
        public string? SensorType { get; set; }
        public bool Status { get; set; }
        public int SensorPackID { get; set; }

        public SensorPack? SensorPack { get; set; }
    }
}
