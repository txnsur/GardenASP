namespace GardenAPI.Models
{
    public class Lectura
    {
        public int ID { get; set; }
        public decimal Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public int SensorID { get; set; }

        public Sensor? Sensor { get; set; }
    }
}
