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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BitchAbout.Views
{
    public class RantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public RantsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Rants
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        [Authorize]
        public async Task<IActionResult> Index()
        {
             ApplicationUser user = await GetCurrentUserAsync();

            RantListViewModel rantListViewModel = new RantListViewModel(_context, user);

            rantListViewModel.Rants = await _context.Rant.Where(users => users.ApplicationUserId == user.Id).ToListAsync();
            return View(rantListViewModel);
        

    }
        // GET: Rants
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        [Authorize]
        public IActionResult ProfRantList()
        {
           
            RantProfRantListViewModel profRantListViewModel = new RantProfRantListViewModel(_context);
            return View(profRantListViewModel);

        }

        // GET: Rants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            RantDetailsViewModel rantDetailsViewModel = new RantDetailsViewModel(_context);
            return View(rantDetailsViewModel);
        }

        // GET: Rants/Create
        [Authorize]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Rants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Create([Bind("Summary, ApplicationUserId")] Rant rant)
        {

            ModelState.Remove("DateCreated");
            if (ModelState.IsValid)
            {
                ApplicationUser user = await GetCurrentUserAsync();
                rant.ApplicationUserId = user.Id;
                var Date = DateTime.Now;
                rant.DateCreated = Date;
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
        public async Task<IActionResult> Edit(int id, [Bind("RantId,Summary,DateCreated,Review")] Rant rant)
        {
            

            if (id != rant.RantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser user = await GetCurrentUserAsync();
                    rant.ApplicationUserId = user.Id;

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
