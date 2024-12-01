using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kowalewski_145204.Data;
using Kowalewski_145204.Models;

namespace Kowalewski_145204.Controllers
{
    public class SamochodsController : Controller
    {
        private readonly DataContext _context;

        public SamochodsController(DataContext context)
        {
            _context = context;
        }

        // GET: Samochods
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NazwaSamochodSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nazwa_desc" : "";
            ViewData["NadwozieSortParm"] = sortOrder == "Nadwozie" ? "nadwozie_desc" : "Nadwozie";
            ViewData["TypSkrzyniBiegowSortParm"] = sortOrder == "TypSkrzyniBiegow" ? "skrzynia_desc" : "TypSkrzyniBiegow";
            ViewData["CenaSortParm"] = sortOrder == "Cena" ? "cena_desc" : "Cena";
            ViewData["ProducentIDSortParm"] = sortOrder == "ProducentID" ? "producent_desc" : "ProducentID";
            ViewData["CurrentFilter"] = searchString;
            var samochody = _context.Samochody
                .Include(s => s.Producent)
                .AsNoTracking();
            if (!String.IsNullOrEmpty(searchString))
            {
                samochody = samochody.Where(s => s.Nazwa.Contains(searchString) || 
                s.TypSkrzyniBiegow.Contains(searchString) || 
                s.Producent.Nazwa.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "nazwa_desc":
                    samochody = samochody.OrderByDescending(s => s.Nazwa);
                    break;
                case "Nadwozie":
                    samochody = samochody.OrderBy(s => s.Nadwozie);
                    break;
                case "nadwozie_desc":
                    samochody = samochody.OrderByDescending(s => s.Nadwozie);
                    break;
                case "TypSkrzyniBiegow":
                    samochody = samochody.OrderBy(s => s.TypSkrzyniBiegow);
                    break;
                case "skrzynia_desc":
                    samochody = samochody.OrderByDescending(s => s.TypSkrzyniBiegow);
                    break;
                case "Cena":
                    samochody = samochody.OrderBy(s => s.Cena);
                    break;
                case "cena_desc":
                    samochody = samochody.OrderByDescending(s => s.Cena);
                    break;
                case "ProducentID":
                    samochody = samochody.OrderBy(s => s.Producent.Nazwa);
                    break;
                case "producent_desc":
                    samochody = samochody.OrderByDescending(s => s.Producent.Nazwa);
                    break;
                default:
                    samochody = samochody.OrderBy(s => s.Nazwa);
                    break;
            }
            return View(await samochody.AsNoTracking().ToListAsync());

            //var dataContext = _context.Samochody.Include(s => s.Producent);
            //return View(await dataContext.ToListAsync());
        }

        // GET: Samochods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Samochody == null)
            {
                return NotFound();
            }

            var samochod = await _context.Samochody
                .Include(s => s.Producent)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (samochod == null)
            {
                return NotFound();
            }

            return View(samochod);
        }

        // GET: Samochods/Create
        public IActionResult Create()
        {
            ViewBag.ProducentID = new SelectList(_context.Producenci.AsNoTracking(), "ID", "Nazwa", null);
            //ViewData["ProducentID"] = new SelectList(_context.Producenci, "ID", "ID");
            return View();
        }

        // POST: Samochods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProducentID,Nazwa,Nadwozie,TypSkrzyniBiegow,Cena")] Samochod samochod)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(samochod);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            ViewBag.ProducentID = new SelectList(_context.Producenci.AsNoTracking(), "ID", "Nazwa", samochod.ProducentID);
            //ViewData["ProducentID"] = new SelectList(_context.Producenci, "ID", "ID", samochod.ProducentID);
            return View(samochod);
        }

        // GET: Samochods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Samochody == null)
            {
                return NotFound();
            }

            var samochod = await _context.Samochody.FindAsync(id);
            if (samochod == null)
            {
                return NotFound();
            }
            ViewBag.ProducentID = new SelectList(_context.Producenci.AsNoTracking(), "ID", "Nazwa", samochod.ProducentID);
            //ViewData["ProducentID"] = new SelectList(_context.Producenci, "ID", "ID", samochod.ProducentID);
            return View(samochod);
        }

        // POST: Samochods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samochodToUpdate = await _context.Samochody.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Samochod>(
                    samochodToUpdate,
                    "",
                    s => s.ProducentID, s => s.Nazwa, s => s.Nadwozie, s => s.TypSkrzyniBiegow, s => s.Cena))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            ViewBag.ProducentID = new SelectList(_context.Producenci.AsNoTracking(), "ID", "Nazwa", samochodToUpdate.ProducentID);
            //ViewData["ProducentID"] = new SelectList(_context.Producenci, "ID", "ID", samochod.ProducentID);
            return View(samochodToUpdate);
        }

        // GET: Samochods/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null || _context.Samochody == null)
            {
                return NotFound();
            }

            var samochod = await _context.Samochody
                .Include(s => s.Producent)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (samochod == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(samochod);
        }

        // POST: Samochods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Samochody == null)
            {
                return Problem("Entity set 'DataContext.Samochody'  is null.");
            }
            var samochod = await _context.Samochody.FindAsync(id);
            if (samochod == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Samochody.Remove(samochod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool SamochodExists(int id)
        {
          return _context.Samochody.Any(e => e.ID == id);
        }
    }
}