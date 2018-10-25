using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BitchAbout.Data;
using BitchAbout.Models;
using BitchAbout.Models.ViewModels;

namespace BitchAbout.Views
{
    public class RantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rants
        public IActionResult Index()
        {
            RantListViewModel rantListViewModel = new RantListViewModel(_context);
            return View(rantListViewModel);

        }
        // GET: Rants
        public IActionResult ProfRantList()
        {
            RantProfRantListViewModel profRantListViewModel = new RantProfRantListViewModel(_context);
            return View(profRantListViewModel);

        }

        // GET: Rants/Details/5
        public async Task<IActionResult> Details()
        {
            return View(await _context.Rant.ToListAsync());
        }

        // GET: Rants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Summary")] Rant rant)
        {
            ModelState.Remove("DateCreated");
            if (ModelState.IsValid)
            {
                rant.DateCreated = DateTime.Now;
                _context.Add(rant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rant);
        }

        // GET: Rants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rant = await _context.Rant.FindAsync(id);
            if (rant == null)
            {
                return NotFound();
            }
            return View(rant);
        }

        // POST: Rants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RantId,DateCreated,Summary,Review")] Rant rant)
        {
            if (id != rant.RantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RantExists(rant.RantId))
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
            return View(rant);
        }

        // GET: Rants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rant = await _context.Rant
                .FirstOrDefaultAsync(m => m.RantId == id);
            if (rant == null)
            {
                return NotFound();
            }

            return View(rant);
        }

        // POST: Rants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rant = await _context.Rant.FindAsync(id);
            _context.Rant.Remove(rant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RantExists(int id)
        {
            return _context.Rant.Any(e => e.RantId == id);
        }
    }
}
