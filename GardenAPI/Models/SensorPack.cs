namespace GardenAPI.Models
{
    public class SensorPack
    {
        public int ID { get; set; }
        public int SensorPackTypeID { get; set; }
        public int? ClientID { get; set; }
        
        public SensorPackType? SensorPackType { get; set; }
        public Usuario? Client { get; set; }
    }
}
