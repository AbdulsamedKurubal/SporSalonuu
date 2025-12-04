using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using SporSalonuu.Data;
using SporSalonuu.Entities;

namespace SporSalonuu.Controllers
{
    public class HesapController : Controller
    {
        private readonly SporSalonuContext _context;

        public HesapController(SporSalonuContext context)
        {
            _context = context;
        }

        // === KAYIT OL ===
        [HttpGet]
        public IActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Kayit(Kullanici model)
        {
            if (_context.Kullanicilar.Any(x => x.Email == model.Email))
            {
                ViewBag.Hata = "Bu e-posta zaten kullanılıyor.";
                return View(model);
            }

            model.Sifre = MD5Sifrele(model.Sifre);
            model.KayitTarihi = DateTime.Now;

            // Sakarya maili ise Yönetici
            if (model.Email.Contains("sakarya.edu.tr"))
                model.YoneticiMi = true;
            else
                model.YoneticiMi = false;

            _context.Kullanicilar.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Giris");
        }

        // === GİRİŞ YAP ===
        [HttpGet]
        public IActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Giris(string email, string sifre)
        {
            var sifreliParola = MD5Sifrele(sifre);
            var kullanici = _context.Kullanicilar.FirstOrDefault(x => x.Email == email && x.Sifre == sifreliParola);

            if (kullanici != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, kullanici.AdSoyad),
                    new Claim(ClaimTypes.Email, kullanici.Email),
                    // Rol ismini de Türkçe yapabiliriz ama Identity genelde Admin/Member kullanır. 
                    // Biz kodda 'Yonetici' ve 'Uye' diyelim.
                    new Claim(ClaimTypes.Role, kullanici.YoneticiMi ? "Yonetici" : "Uye"),
                    new Claim(ClaimTypes.NameIdentifier, kullanici.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Hata = "E-posta veya şifre hatalı!";
            return View();
        }

        // === ÇIKIŞ YAP ===
        public async Task<IActionResult> Cikis()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Giris");
        }

        private string MD5Sifrele(string text)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(text);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes);
            }
        }
    }
}