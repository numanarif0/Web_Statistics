using Microsoft.AspNetCore.Mvc;
using YokIstatistikWeb.Models;
using MongoDB.Driver;

namespace YokIstatistikWeb.Controllers
{
    [Route("Yuksekogretim")]
    public class YuksekogretimController : Controller
    {
        private readonly MongoDbContext _context;

        public YuksekogretimController(MongoDbContext context)
        {
            _context = context;
        }

        [Route("")]
        public IActionResult Index(string search, string sehir, string tur, string year = "2024_2025")
        {
            try
            {
                var collection = _context.GetCollectionForYear(year);
                var veriler = collection.Find(_ => true).ToList();

                if (!string.IsNullOrEmpty(search))
                    veriler = veriler.Where(u => u.universite.ToLower().Contains(search.ToLower())).ToList();

                if (!string.IsNullOrEmpty(sehir))
                    veriler = veriler.Where(u => u.sehir == sehir).ToList();

                if (!string.IsNullOrEmpty(tur))
                    veriler = veriler.Where(u => u.tur == tur).ToList();

                ViewBag.Yil = year.Replace("_", "-");
                ViewBag.Sehirler = veriler.Select(u => u.sehir).Distinct().OrderBy(s => s).ToList();
                ViewBag.Turler = veriler.Select(u => u.tur).Distinct().OrderBy(t => t).ToList();

                return View(veriler);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Veri çekilirken bir hata oluştu.";
                return View(new List<Universite>());
            }
        }

        [Route("Detay")]
        public IActionResult Detay(string id, string year = "2024_2025")
        {
            try
            {
                var collection = _context.GetCollectionForYear(year);
                var universite = collection.Find(u => u.Id == id).FirstOrDefault();

                if (universite == null)
                {
                    return NotFound();
                }

                ViewBag.Yil = year.Replace("_", "-");
                return View(universite);
            }
            catch (Exception)
            {
                TempData["Error"] = "Detay bilgileri çekilirken bir hata oluştu.";
                return RedirectToAction("Index", new { year });
            }
        }

        [Route("Karsilastir")]
        public IActionResult Karsilastir(string id1, string id2, string year = "2024_2025")
        {
            try
            {
                var collection = _context.GetCollectionForYear(year);
                var u1 = collection.Find(u => u.Id == id1).FirstOrDefault();
                var u2 = collection.Find(u => u.Id == id2).FirstOrDefault();

                if (u1 == null || u2 == null)
                {
                    return NotFound();
                }

                ViewBag.Yil = year.Replace("_", "-");
                return View(Tuple.Create(u1, u2));
            }
            catch (Exception)
            {
                TempData["Error"] = "Karşılaştırma yapılırken bir hata oluştu.";
                return RedirectToAction("Index", new { year });
            }
        }
    }
}
