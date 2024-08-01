namespace GardenAPI.Models
{
    public class UserMembership
    {
        public int ID { get; set; }
        public bool Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ClientID { get; set; }
        public int MembershipID { get; set; }
        public decimal? FinalPrice { get; set; }

        public Usuario? Client { get; set; }
        public Membresia? Membership { get; set; }
    }
}
