using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskShare.Algorithms;
using TaskShare.Models;

namespace TaskShare.Controllers
{
    public class IssuesController : Controller
    {
        private readonly DataContext _context;

        public IssuesController(DataContext context)
        {
            _context = context;
        }

        // GET: Issues
        public async Task<IActionResult> Index()
        {
              return View(await _context.Issues.ToListAsync());
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Issues == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // GET: Issues/Create
        public IActionResult Create()
        {
            ViewBag.ListOfPseudonyms = _context.Users.Select(u => u.Pseudonym).ToList();
            ViewBag.ListOfLabels = _context.Tasks.Select(u => u.Label).ToList();
            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Label,Description")] Issue issue, [Bind("Users")] string Users, [Bind("Tasks")] string Tasks)
        {
            if (ModelState.IsValid)
            {
                issue = _context.Issues.Include("Users")
                .Include("Tasks").FirstOrDefault(m => m.Id == issue.Id);
                var listOfUsers = Users
                .Split(",")
                .Select(pseudo => pseudo.Trim())
                .Join(_context.Users,
                pseudo => pseudo,
                user => user.Pseudonym,
                (pseudo, user) => user)
                .ToList();
                var listOfTasks = Tasks
                .Split(",")
                .Select(desc => desc.Trim())
                .Join(_context.Tasks,
                desc => desc,
                bill => bill.Label,
                (desc, bill) => bill)
                .ToList();
                issue.Users = listOfUsers;
                issue.Tasks = listOfTasks;
                _context.Issues.Update(issue);
                _context.SaveChanges();

                _context.Add(issue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }

        public async Task<IActionResult> Suggest(int? id)
        {
            if (id == null || _context.Issues == null)
            {
                return NotFound();
            }
                var issue = await _context.Issues
                .Include("Users")
                .Include("Tasks")
                .FirstOrDefaultAsync(i => i.Id == id);
                if (issue == null)
            {
                return NotFound();
            }
            var algorithm = new BipartitionAlgorithm<Models.Task>();
            algorithm.SubsetsCount = issue.Users.Count();
            algorithm.Predicate = i => i.TimeCost;
            ViewBag.Result = algorithm.Run(issue.Tasks);
            return View(issue);
        }


        // GET: Issues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Issues == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Label,Description")] Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.Id))
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
            return View(issue);
        }

        // GET: Issues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Issues == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Issues == null)
            {
                return Problem("Entity set 'DataContext.Issues'  is null.");
            }
            var issue = await _context.Issues.FindAsync(id);
            if (issue != null)
            {
                _context.Issues.Remove(issue);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
          return _context.Issues.Any(e => e.Id == id);
        }
    }
}
