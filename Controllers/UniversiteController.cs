using Microsoft.AspNetCore.Mvc;
using YokIstatistikWeb.Services;
using YokIstatistikWeb.Models;

namespace YokIstatistikWeb.Controllers
{
    [Route("Yuksekogretim")]
    public class UniversiteController : Controller
    {
        private readonly UniversiteService _universiteService;

        public UniversiteController(UniversiteService universiteService)
        {
            _universiteService = universiteService;
        }
        [Route("")]
        public IActionResult Index(string search, string sehir, string tur, string yil)
        {
            var veriler = _universiteService.GetAll();

            if (!string.IsNullOrEmpty(search))
                veriler = veriler.Where(u => u.universite.ToLower().Contains(search.ToLower())).ToList();

            if (!string.IsNullOrEmpty(sehir))
                veriler = veriler.Where(u => u.sehir == sehir).ToList();

            if (!string.IsNullOrEmpty(tur))
                veriler = veriler.Where(u => u.tur == tur).ToList();

            // Yıl parametresi varsa başlığı güncelle
            if (!string.IsNullOrEmpty(yil))
            {
                ViewBag.Yil = yil;
            }
            else
            {
                ViewBag.Yil = "2023-2024"; // Varsayılan yıl
            }

            // Şehir ve tür dropdown'ları için veriler ViewBag ile gönderilir
            ViewBag.Sehirler = _universiteService.GetAll().Select(u => u.sehir).Distinct().OrderBy(s => s).ToList();
            ViewBag.Turler = _universiteService.GetAll().Select(u => u.tur).Distinct().OrderBy(t => t).ToList();

            return View(veriler);
        }

        [Route("Detay")]
        // ✅ Detay Sayfası Aksiyonu
        public IActionResult Detay(string isim)
        {
            var universite = _universiteService.GetAll()
                .FirstOrDefault(u => u.universite == isim);

            if (universite == null)
            {
                return NotFound();
            }

            return View(universite);
        }
        [Route("Karsilastir")]
        public IActionResult Karsilastir(string uni1, string uni2)
        {
            var tumVeriler = _universiteService.GetAll();

            var u1 = tumVeriler.FirstOrDefault(u => u.universite == uni1);
            var u2 = tumVeriler.FirstOrDefault(u => u.universite == uni2);

            
            if (u1 == null || u2 == null)
                return NotFound("Üniversite bulunamadı");

            var model = new Tuple<Universite, Universite>(u1, u2);
            return View(model);
        }

    }
}
