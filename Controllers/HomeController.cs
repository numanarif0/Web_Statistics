using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using YokIstatistikWeb.Models;
using YokIstatistikWeb.Services;

namespace YokIstatistikWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UniversiteService _universiteService;

        public HomeController(ILogger<HomeController> logger, UniversiteService universiteService)
        {
            _logger = logger;
            _universiteService = universiteService ?? throw new ArgumentNullException(nameof(universiteService));
        }

        [Route("")]
        public IActionResult Index()
        {
            var universiteler = _universiteService.GetAll();
            
            var devlet = universiteler.Count(u => u.tur.ToUpper() == "DEVLET");
            var vakif = universiteler.Count(u => u.tur.ToUpper() == "VAKIF");
            var vakifMyo = universiteler.Count(u => u.tur.ToUpper().Contains("VAKIF MYO") || u.tur.ToUpper().Contains("MYO"));
            
            // Tabloda vakıflar birleşik gösterilecek
            
            var toplam = universiteler.Count;

            ViewBag.Devlet = devlet;
            ViewBag.Vakif = vakif;
            ViewBag.VakifMyo = vakifMyo;
            ViewBag.Toplam = toplam;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}