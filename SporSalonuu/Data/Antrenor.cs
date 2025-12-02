namespace SporSalonuu.Entities
{
    public class Antrenor
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string UzmanlikAlani { get; set; }

        public virtual ICollection<Hizmet> Hizmetler { get; set; }
        public string TamAd => $"{Ad} {Soyad}";
    }
}