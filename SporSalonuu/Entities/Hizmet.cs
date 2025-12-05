namespace SporSalonuu.Entities
{
    public class Hizmet
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int SureDakika { get; set; }
        public decimal Ucret { get; set; }

        public int SalonId { get; set; }
        public virtual Salon? Salon { get; set; }
        public virtual ICollection<Antrenor>? Antrenorler { get; set; }
    }
}