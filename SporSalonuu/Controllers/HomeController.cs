using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SporSalonuu.Data;
using SporSalonuu.Entities;
using System.Diagnostics;

namespace SporSalonuu.Controllers
{
    public class HomeController : Controller
    {
        private readonly SporSalonuContext _context;

        public HomeController(SporSalonuContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Anasayfada dersleri (Hizmetleri) listeleyelim.
            // Yanýnda hangi salona ait olduðunu da getiriyoruz.
            var dersler = _context.Hizmetler.Include(x => x.Salon).ToList();
            return View(dersler);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}