using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvCreating.Data;

namespace EvCreating.Controllers
{
    public class MeningsController : Controller
    {
        private readonly EvCreatingContext _context;

        public MeningsController(EvCreatingContext context)
        {
            _context = context;
        }

        // GET: Menings
        public async Task<IActionResult> Index()
        {
            var evCreatingContext = _context.Mening.Include(m => m.Evenement);
            return View(await evCreatingContext.ToListAsync());
        }

        // GET: Menings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mening == null)
            {
                return NotFound();
            }

            var mening = await _context.Mening
                .Include(m => m.Evenement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mening == null)
            {
                return NotFound();
            }

            return View(mening);
        }

        // GET: Menings/Create
        public IActionResult Create()
        {
            ViewData["EvenementId"] = new SelectList(_context.Evenement, "ID", "ID");
            return View();
        }

        // POST: Menings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Inhoud,Naam,Datum,Rating,EvenementNaam,EvenementId")] Mening mening)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mening);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EvenementId"] = new SelectList(_context.Evenement, "ID", "ID", mening.EvenementId);
            return View(mening);
        }

        // GET: Menings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mening == null)
            {
                return NotFound();
            }

            var mening = await _context.Mening.FindAsync(id);
            if (mening == null)
            {
                return NotFound();
            }
            ViewData["EvenementId"] = new SelectList(_context.Evenement, "ID", "ID", mening.EvenementId);
            return View(mening);
        }

        // POST: Menings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Inhoud,Naam,Datum,Rating,EvenementNaam,EvenementId")] Mening mening)
        {
            if (id != mening.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mening);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeningExists(mening.Id))
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
            ViewData["EvenementId"] = new SelectList(_context.Evenement, "ID", "ID", mening.EvenementId);
            return View(mening);
        }

        // GET: Menings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mening == null)
            {
                return NotFound();
            }

            var mening = await _context.Mening
                .Include(m => m.Evenement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mening == null)
            {
                return NotFound();
            }

            return View(mening);
        }

        // POST: Menings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mening == null)
            {
                return Problem("Entity set 'EvCreatingContext.Mening'  is null.");
            }
            var mening = await _context.Mening.FindAsync(id);
            if (mening != null)
            {
                _context.Mening.Remove(mening);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeningExists(int id)
        {
          return (_context.Mening?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
