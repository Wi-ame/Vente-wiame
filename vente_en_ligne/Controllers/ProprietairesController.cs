using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vente_en_ligne.Data;
using vente_en_ligne.Models;

namespace vente_en_ligne.Controllers
{
    public class ProprietairesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProprietairesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> PropriHome()
        {
            return View();
        }
        // GET: Proprietaires
        public async Task<IActionResult> Index(string cin,string password)
        {
            bool authentificationReussie = AuthentificationReussie(cin, password);

            // Si l'authentification réussit, redirigez vers la vue "Create" du contrôleur "Produit"
            if (authentificationReussie)
            {
                // Remplacez "NomDuControleur" par le vrai nom de votre contrôleur "Produit"
                return RedirectToAction("Create", "Produits");
            }
            return _context.Proprietaires != null ? 
                          View(await _context.Proprietaires.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Proprietaires'  is null.");
        }
        private bool AuthentificationReussie(string cin, string password)
        {
            // Votre logique de recherche dans la base de données
            // Assurez-vous que le mot de passe est stocké de manière sécurisée, par exemple en utilisant le hachage

            // Exemple fictif :
            var proprietaire = _context.Proprietaires
                .FirstOrDefault(p => p.INterID == cin && p.password == password);

            // Retournez true si un propriétaire correspondant est trouvé, sinon retournez false
            return proprietaire != null;
        }
        // GET: Proprietaires/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Proprietaires == null)
            {
                return NotFound();
            }

            var proprietaire = await _context.Proprietaires
                .FirstOrDefaultAsync(m => m.INterID == id);
            if (proprietaire == null)
            {
                return NotFound();
            }

            return View(proprietaire);
        }

        // GET: Proprietaires/Create
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> CreateProp()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProp([Bind("INterID,NomEntreprise,AdresseEntreprise,Nom,Prenom,Tele,Email,password")] Proprietaire proprietaire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proprietaire);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(CreateProp)); // Rediriger vers la même action pour afficher le message
            }

            // Si le modèle n'est pas valide, retourner la vue sans stocker de message
            return View(proprietaire);
        }

        // POST: Proprietaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("INterID,NomEntreprise,AdresseEntreprise,Nom,Prenom,Tele,Email,password")] Proprietaire proprietaire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proprietaire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proprietaire);
        }

        // GET: Proprietaires/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Proprietaires == null)
            {
                return NotFound();
            }

            var proprietaire = await _context.Proprietaires.FindAsync(id);
            if (proprietaire == null)
            {
                return NotFound();
            }
            return View(proprietaire);
        }

        // POST: Proprietaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("INterID,NomEntreprise,AdresseEntreprise,Nom,Prenom,Tele,Email,password")] Proprietaire proprietaire)
        {
            if (id != proprietaire.INterID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proprietaire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProprietaireExists(proprietaire.INterID))
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
            return View(proprietaire);
        }
        public async Task<IActionResult> DeleteProprio()
        {
            return PartialView("_DeleteProprio");
        }
        public async Task<IActionResult> ModifyProprio()
        {
            return PartialView("_ModifyProprio");
        }
        public async Task<IActionResult> ModifyForm()
        {
            return PartialView("_ModifyForm");
        }
        // GET: Proprietaires/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Proprietaires == null)
            {
                return NotFound();
            }

            var proprietaire = await _context.Proprietaires
                .FirstOrDefaultAsync(m => m.INterID == id);
            if (proprietaire == null)
            {
                return NotFound();
            }

            return View(proprietaire);
        }

        // POST: Proprietaires/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("INterID")] Proprietaire proprietaire)
        {
            try
            {
                if (_context.Proprietaires == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Proprietaires' is null.");
                }

                // Retrieve the proprietaire by ID
                var existingProprietaire = await _context.Proprietaires.FindAsync(proprietaire.INterID);

                if (existingProprietaire == null)
                {
                    return NotFound(); // Return 404 if the proprietaire is not found
                }

                _context.Proprietaires.Remove(existingProprietaire);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(DeleteProprio));
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return Problem($"An error occurred while deleting: {ex.Message}");
            }
        }
        private bool ProprietaireExists(string id)
        {
          return (_context.Proprietaires?.Any(e => e.INterID == id)).GetValueOrDefault();
        }
    }
}
