using System.ComponentModel.DataAnnotations;

namespace SporSalonuu.Entities
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public string Telefon { get; set; }
        public bool YoneticiMi { get; set; } // Admin mi?
        public DateTime KayitTarihi { get; set; } = DateTime.Now;
    }
}