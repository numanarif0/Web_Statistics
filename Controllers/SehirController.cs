using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using YokIstatistikWeb.Models;
using Newtonsoft.Json; // varsa




public class SehirController : Controller
{
    private readonly MongoDbContext _context;
    public SehirController(MongoDbContext context)
    {
        _context = context;
    }

    public IActionResult Grafik()
    {
        var liste = _context.Universiteler.Find(_ => true).ToList();

        System.Diagnostics.Debug.WriteLine("MongoDB'den gelen üniversite sayısı: " + liste.Count);


        var veriler = liste
            .GroupBy(u => u.sehir)
            .Select(g => new SehirVeriViewModel
            {
                Sehir = g.Key,
                Toplam = g
                    .SelectMany(u => u.birimler ?? new List<Birim>()) // <-- null koruması
                    .Sum(b => b.toplam_toplam ?? 0)

            })
            .ToList();
       


        return View(veriler);
    }



}
