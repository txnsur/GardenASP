namespace GardenAPI.Models
{
    public class Venta
    {
        public int ID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int ClientID { get; set; }
        public int SensorPackID { get; set; }

        public Usuario? Client { get; set; }
        public SensorPack? SensorPack { get; set; }
    }
}
