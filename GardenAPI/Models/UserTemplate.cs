namespace GardenAPI.Models
{
    public class UserTemplate
    {
        public int UserID { get; set; }
        public int TemplateID { get; set; }

        public Usuario? User { get; set; }
        public Plantilla? Template { get; set; }
    }
}
