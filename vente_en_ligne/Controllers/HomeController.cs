using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using vente_en_ligne.Data;
using vente_en_ligne.Models;

namespace vente_en_ligne.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var produits = _context.Produits
                .Select(p => new Produit
                {
                    Name = p.Name,
                    ImageData = p.ImageData,
                    prix = p.prix
                })
                .ToList();

            return View(produits);
        }
        [HttpPost]
        public IActionResult FilterByPrice(double minPrice, double maxPrice)
        {
            var filteredProduits = _context.Produits
                .Where(p => p.prix >= minPrice && p.prix <= maxPrice)
                .ToList();

            return View("Index", filteredProduits);
        }

        public  IActionResult ProductPrice()
        {
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