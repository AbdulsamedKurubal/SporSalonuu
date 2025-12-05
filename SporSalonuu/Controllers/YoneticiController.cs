using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SporSalonuu.Data;
using SporSalonuu.Entities;

namespace SporSalonuu.Controllers
{
    // DİKKAT: Bu satır sayesinde buraya sadece "Yonetici" rolü olanlar girebilir!
    [Authorize(Roles = "Yonetici")]
    public class YoneticiController : Controller
    {
        private readonly SporSalonuContext _context;

        public YoneticiController(SporSalonuContext context)
        {
            _context = context;
        }

        // Admin Paneli Anasayfası
        public IActionResult Index()
        {
            return View();
        }

        // ==========================================
        //  🏋️ SPOR SALONU YÖNETİMİ
        // ==========================================

        // 1. Salonları Listele
        public IActionResult SalonListesi()
        {
            var salonlar = _context.Salonlar.ToList();
            return View(salonlar);
        }

        // 2. Yeni Salon Ekleme Sayfası (GET)
        [HttpGet]
        public IActionResult SalonEkle()
        {
            return View();
        }

        // 3. Yeni Salon Kaydetme İşlemi (POST)
        [HttpPost]
        public IActionResult SalonEkle(Salon salon)
        {
            _context.Salonlar.Add(salon);
            _context.SaveChanges();
            return RedirectToAction("SalonListesi");
        }

        // 4. Salon Silme
        public IActionResult SalonSil(int id)
        {
            var salon = _context.Salonlar.Find(id);
            if (salon != null)
            {
                _context.Salonlar.Remove(salon);
                _context.SaveChanges();
            }
            return RedirectToAction("SalonListesi");
        }


        // ==========================================
        //  💪 ANTRENÖR (EĞİTMEN) YÖNETİMİ
        // ==========================================

        // 1. Antrenörleri Listele
        public IActionResult AntrenorListesi()
        {
            var antrenorler = _context.Antrenorler.ToList();
            return View(antrenorler);
        }

        // 2. Yeni Antrenör Ekleme Sayfası (GET)
        [HttpGet]
        public IActionResult AntrenorEkle()
        {
            return View();
        }

        // 3. Yeni Antrenör Kaydetme İşlemi (POST)
        [HttpPost]
        public IActionResult AntrenorEkle(Antrenor antrenor)
        {
            // Validasyon kontrolü: Ad, Soyad vb. dolu mu?
            if (ModelState.IsValid)
            {
                _context.Antrenorler.Add(antrenor);
                _context.SaveChanges();
                return RedirectToAction("AntrenorListesi");
            }
            // Hata varsa formu tekrar göster
            return View(antrenor);
        }

        // 4. Antrenör Silme
        public IActionResult AntrenorSil(int id)
        {
            var antrenor = _context.Antrenorler.Find(id);
            if (antrenor != null)
            {
                _context.Antrenorler.Remove(antrenor);
                _context.SaveChanges();
            }
            return RedirectToAction("AntrenorListesi");
        }
    }
}
