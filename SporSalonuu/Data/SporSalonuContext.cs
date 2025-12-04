using Microsoft.EntityFrameworkCore;
using SporSalonuu.Entities;

namespace SporSalonuu.Data
{
    public class SporSalonuContext : DbContext
    {
        public SporSalonuContext(DbContextOptions<SporSalonuContext> options) : base(options)
        {
        }

        // Tablo İsimleri de Türkçe Oldu
        public DbSet<Salon> Salonlar { get; set; }
        public DbSet<Antrenor> Antrenorler { get; set; }
        public DbSet<Hizmet> Hizmetler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
    }
}