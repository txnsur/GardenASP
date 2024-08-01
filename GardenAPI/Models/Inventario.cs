namespace GardenAPI.Models
{
    public class Inventario
    {
        public int ID { get; set; }
        public int SensorPackTypeID { get; set; }
        public int Stock { get; set; }

        public SensorPackType? SensorPackType { get; set; }
    }
}
