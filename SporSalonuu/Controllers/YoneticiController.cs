using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Include işlemi için gerekli
using SporSalonuu.Data;
using SporSalonuu.Entities;

namespace SporSalonuu.Controllers
{
    // 🛡️ GÜVENLİK: Sadece "Yonetici" yetkisi olanlar buraya girebilir.
    [Authorize(Roles = "Yonetici")]
    public class YoneticiController : Controller
    {
        private readonly SporSalonuContext _context;

        public YoneticiController(SporSalonuContext context)
        {
            _context = context;
        }

        // === 🏠 PANEL ANASAYFASI ===
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

        // 2. Yeni Salon Ekle (Sayfa)
        [HttpGet]
        public IActionResult SalonEkle()
        {
            return View();
        }

        // 3. Yeni Salon Kaydet (İşlem)
        [HttpPost]
        public IActionResult SalonEkle(Salon salon)
        {
            if (ModelState.IsValid)
            {
                _context.Salonlar.Add(salon);
                _context.SaveChanges();
                return RedirectToAction("SalonListesi");
            }
            return View(salon);
        }

        // 4. Salon Sil
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

        // 2. Yeni Antrenör Ekle (Sayfa)
        [HttpGet]
        public IActionResult AntrenorEkle()
        {
            return View();
        }

        // 3. Yeni Antrenör Kaydet (İşlem)
        [HttpPost]
        public IActionResult AntrenorEkle(Antrenor antrenor)
        {
            if (ModelState.IsValid)
            {
                _context.Antrenorler.Add(antrenor);
                _context.SaveChanges();
                return RedirectToAction("AntrenorListesi");
            }
            return View(antrenor);
        }

        // 4. Antrenör Sil
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


        // ==========================================
        //  🧘 HİZMET (DERS) YÖNETİMİ
        // ==========================================

        // 1. Hizmetleri Listele
        public IActionResult HizmetListesi()
        {
            // Hangi salona ait olduğunu da getiriyoruz (Join işlemi)
            var hizmetler = _context.Hizmetler.Include(x => x.Salon).ToList();
            return View(hizmetler);
        }

        // 2. Yeni Hizmet Ekle (Sayfa)
        [HttpGet]
        public IActionResult HizmetEkle()
        {
            // Dropdown (Açılır kutu) için salonları gönderiyoruz
            ViewBag.Salonlar = _context.Salonlar.ToList();
            return View();
        }

        // 3. Yeni Hizmet Kaydet (İşlem)
        [HttpPost]
        public IActionResult HizmetEkle(Hizmet hizmet)
        {
            if (ModelState.IsValid)
            {
                _context.Hizmetler.Add(hizmet);
                _context.SaveChanges();
                return RedirectToAction("HizmetListesi");
            }

            // Hata varsa salon listesini tekrar yükle ki kutu boş kalmasın
            ViewBag.Salonlar = _context.Salonlar.ToList();
            return View(hizmet);
        }

        // 4. Hizmet Sil
        public IActionResult HizmetSil(int id)
        {
            var hizmet = _context.Hizmetler.Find(id);
            if (hizmet != null)
            {
                _context.Hizmetler.Remove(hizmet);
                _context.SaveChanges();
            }
            return RedirectToAction("HizmetListesi");
        }
    }
}