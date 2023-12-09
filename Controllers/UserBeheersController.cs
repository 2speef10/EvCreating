using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvCreating.Data;
using EvCreating.Models;

namespace EvCreating.Controllers
{
    public class UserBeheersController : Controller
    {
        private readonly EvCreatingContext _context;

        public UserBeheersController(EvCreatingContext context)
        {
            _context = context;
        }

        // GET: UserBeheers
        public async Task<IActionResult> Index()
        {
              return _context.UserBeheer != null ? 
                          View(await _context.UserBeheer.ToListAsync()) :
                          Problem("Entity set 'EvCreatingContext.UserBeheer'  is null.");
        }

        // GET: UserBeheers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserBeheer == null)
            {
                return NotFound();
            }

            var userBeheer = await _context.UserBeheer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userBeheer == null)
            {
                return NotFound();
            }

            return View(userBeheer);
        }

        // GET: UserBeheers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserBeheers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Voornaam,Achernaam,Email,TelNummer")] UserBeheer userBeheer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userBeheer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userBeheer);
        }

        // GET: UserBeheers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserBeheer == null)
            {
                return NotFound();
            }

            var userBeheer = await _context.UserBeheer.FindAsync(id);
            if (userBeheer == null)
            {
                return NotFound();
            }
            return View(userBeheer);
        }

        // POST: UserBeheers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Voornaam,Achernaam,Email,TelNummer")] UserBeheer userBeheer)
        {
            if (id != userBeheer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userBeheer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserBeheerExists(userBeheer.ID))
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
            return View(userBeheer);
        }

        // GET: UserBeheers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserBeheer == null)
            {
                return NotFound();
            }

            var userBeheer = await _context.UserBeheer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userBeheer == null)
            {
                return NotFound();
            }

            return View(userBeheer);
        }

        // POST: UserBeheers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserBeheer == null)
            {
                return Problem("Entity set 'EvCreatingContext.UserBeheer'  is null.");
            }
            var userBeheer = await _context.UserBeheer.FindAsync(id);
            if (userBeheer != null)
            {
                _context.UserBeheer.Remove(userBeheer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserBeheerExists(int id)
        {
          return (_context.UserBeheer?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
