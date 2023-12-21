using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vente_en_ligne.Data;
using vente_en_ligne.Models;

namespace vente_en_ligne.Controllers
{
    public class ProduitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProduitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produits
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Produits.Include(p => p.Categories).Include(p => p.proprietaires);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> DeleteProduct()
        {
            return View();
        }

        public async Task<IActionResult> ModifyPr()
        {
            return View();
        }
       

        // GET: Produits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produits == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits
                .Include(p => p.Categories)
                .Include(p => p.proprietaires)
                .FirstOrDefaultAsync(m => m.IdPr == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }
        public async Task<IActionResult> CreateProduct()

        {
            var categoriesList = _context.Categories
                .Select(c => new SelectListItem { Value = c.CategorieID.ToString(), Text = c.CategorieName })
                .ToList();


            ViewData["CategoriesList"] = categoriesList;

            return PartialView("_CreateProduct");
        }
        public async Task<IActionResult> RemoveProduct()
        {
            return PartialView("_RemoveProduct");
        }
        public async Task<IActionResult> ModifyProduct()
        {
            return PartialView("_ModifyProduct");
        }
        public async Task<IActionResult> ModifyProductForm()
        {
            return PartialView("_ModifyFormProduct");
        }

        // GET: Produits/Create

        public async Task<IActionResult> Create()
        {
            // Récupérer les catégories depuis la base de données
            var categoriesList = _context.Categories
                .Select(c => new SelectListItem { Value = c.CategorieID.ToString(), Text = c.CategorieName })
                .ToList();


            ViewData["CategoriesList"] = categoriesList;


            return View();
        }

        // POST: Produits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPr,Name,Description,prix,IDC,DateDepot,ImageFile,stock,IDP")] Produit produit)
        {
            try
            {
                Console.WriteLine($"IDP: {produit.IDP}");
                Console.WriteLine($"IDC: {produit.IDC}");

                // Assurez-vous que le champ "Category" a une valeur sélectionnée
                if (produit.IDC > 0)
                {
                    // Gérer l'envoi de l'image
                    if (produit.ImageFile != null && produit.ImageFile.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await produit.ImageFile.CopyToAsync(stream);
                            produit.ImageData = stream.ToArray();
                        }
                        if (produit.ImageData != null)
                        {
                            Console.WriteLine($"ImageData Length: {produit.ImageData.Length} bytes");
                        }
                        else
                        {
                            Console.WriteLine("No image data");
                        }
                    }


                    _context.Add(produit);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("IDC", "Please select a category.");
                }
            }
            catch (Exception ex)
            {
                // Gérer l'exception, par exemple, afficher dans la console de débogage
                Console.WriteLine(ex.Message);
                // Rediriger vers une page d'erreur ou une autre action en cas d'erreur
                return RedirectToAction("Error");
            }

            // Si la validation échoue, assurez-vous de récupérer à nouveau la liste des catégories
            var categoriesList = await _context.Categories
                .Select(c => new SelectListItem { Value = c.CategorieID.ToString(), Text = c.CategorieName })
                .ToListAsync();

            ViewData["CategoriesList"] = categoriesList;

            // Vous pouvez également réinitialiser la liste dans le modèle Produit
            produit.CategoriesList = categoriesList;

            return View(produit);
        }


        // GET: Produits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produits == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits.FindAsync(id);
            if (produit == null)
            {
                return NotFound();
            }
            ViewData["IDC"] = new SelectList(_context.Categories, "CategorieID", "CategorieID", produit.IDC);
            ViewData["IDP"] = new SelectList(_context.Proprietaires, "INterID", "INterID", produit.IDP);
            return View(produit);
        }

        // POST: Produits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPr,Name,Description,prix,IDC,IDP,DateDepot,ImageData,stock")] Produit produit)
        {
            if (id != produit.IdPr)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduitExists(produit.IdPr))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IDC"] = new SelectList(_context.Categories, "CategorieID", "CategorieID", produit.IDC);
            ViewData["IDP"] = new SelectList(_context.Proprietaires, "INterID", "INterID", produit.IDP);
            return View(produit);
        }

        // GET: Produits/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produits == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits
                .Include(p => p.Categories)
                .Include(p => p.proprietaires)
                .FirstOrDefaultAsync(m => m.IdPr == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produits == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Produits'  is null.");
            }
            var produit = await _context.Produits.FindAsync(id);
            if (produit != null)
            {
                _context.Produits.Remove(produit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduitExists(int id)
        {
          return (_context.Produits?.Any(e => e.IdPr == id)).GetValueOrDefault();
        }
    }
}
