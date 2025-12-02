namespace SporSalonuu.Entities
{
    public class Randevu
    {
        public int Id { get; set; }
        public DateTime TarihSaat { get; set; }
        public string Durum { get; set; } = "Bekliyor";

        public int AntrenorId { get; set; }
        public virtual Antrenor Antrenor { get; set; }

        public int KullaniciId { get; set; }
        public virtual Kullanici Kullanici { get; set; }

        public int HizmetId { get; set; }
        public virtual Hizmet Hizmet { get; set; }
    }
}