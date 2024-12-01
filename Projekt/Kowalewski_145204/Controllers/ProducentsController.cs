using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kowalewski_145204.Data;
using Kowalewski_145204.Models;
using Microsoft.Data.SqlClient;

namespace Kowalewski_145204.Controllers
{
    public class ProducentsController : Controller
    {
        private readonly DataContext _context;

        public ProducentsController(DataContext context)
        {
            _context = context;
        }

        // GET: Producents
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NazwaSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nazwa_desc" : "";
            ViewData["KrajSortParm"] = sortOrder == "Kraj" ? "kraj_desc" : "Kraj";
            ViewData["RokZalozeniaSortParm"] = sortOrder == "RokZalozenia" ? "rok_zalozenia_desc" : "RokZalozenia";
            ViewData["CurrentFilter"] = searchString;
            var producenci = from p in _context.Producenci
                              select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                producenci = producenci.Where(p => p.Nazwa.Contains(searchString) || p.Kraj.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "nazwa_desc":
                    producenci = producenci.OrderByDescending(p => p.Nazwa);
                    break;
                case "Kraj":
                    producenci = producenci.OrderBy(p => p.Kraj);
                    break;
                case "kraj_desc":
                    producenci = producenci.OrderByDescending(p => p.Kraj);
                    break;
                case "RokZalozenia":
                    producenci = producenci.OrderBy(p => p.RokZalozenia.Length).ThenBy(p => p.RokZalozenia);
                    break;
                case "rok_zalozenia_desc":
                    producenci = producenci.OrderByDescending(p => p.RokZalozenia.Length).OrderByDescending(p => p.RokZalozenia);
                    break;
                default:
                    producenci = producenci.OrderBy(p => p.Nazwa);
                    break;
            }
            return View(await producenci.AsNoTracking().ToListAsync());
        }

        // GET: Producents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Producenci == null)
            {
                return NotFound();
            }

            var producent = await _context.Producenci
                .Include(s => s.Samochody)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (producent == null)
            {
                return NotFound();
            }

            return View(producent);
        }

        // GET: Producents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nazwa,Kraj,RokZalozenia")] Producent producent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(producent);
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
            return View(producent);
        }

        // GET: Producents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Producenci == null)
            {
                return NotFound();
            }

            var producent = await _context.Producenci.FindAsync(id);
            if (producent == null)
            {
                return NotFound();
            }
            return View(producent);
        }

        // POST: Producents/Edit/5
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

            var producentToUpdate = await _context.Producenci.FirstOrDefaultAsync(p => p.ID == id);
            if (await TryUpdateModelAsync<Producent>(
                    producentToUpdate,
                    "",
                    p => p.Nazwa, p => p.Kraj, p => p.RokZalozenia))
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
            return View(producentToUpdate);
        }

        // GET: Producents/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null || _context.Producenci == null)
            {
                return NotFound();
            }

            var producent = await _context.Producenci
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (producent == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(producent);
        }

        // POST: Producents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Producenci == null)
            {
                return Problem("Entity set 'DataContext.Producenci'  is null.");
            }
            var producent = await _context.Producenci.FindAsync(id);
            if (producent == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Producenci.Remove(producent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool ProducentExists(int id)
        {
          return _context.Producenci.Any(e => e.ID == id);
        }
    }
}
