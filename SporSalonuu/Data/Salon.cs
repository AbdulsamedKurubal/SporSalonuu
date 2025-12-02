using System.ComponentModel.DataAnnotations;

namespace SporSalonuu.Entities
{
    public class Salon
    {
        public int Id { get; set; }
        public string Ad { get; set; } // Salon Adı
        public string Adres { get; set; }
        public TimeSpan AcilisSaati { get; set; }
        public TimeSpan KapanisSaati { get; set; }

        public virtual ICollection<Hizmet> Hizmetler { get; set; }
    }
}