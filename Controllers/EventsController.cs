using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using EvCreating.Data;
using EvCreating.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvCreating.Controllers
{
    public class EventsController : Controller
    {
        private readonly EvCreatingContext _context;

        public EventsController(EvCreatingContext context)
        {
            _context = context;
        }

        public IActionResult BeoordeelEvenement()
        {
            var evenementen = _context.Event.Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.Naam }).ToList();
            ViewBag.Eventen = evenementen;
            return View();
        }

        public async Task<IActionResult> Index(int? selectedMonth)
        {
            var events = _context.Event.Where(e => !e.IsDeleted).AsQueryable();

            if (selectedMonth.HasValue && selectedMonth > 0 && selectedMonth <= 12)
            {
                events = events.Where(e => e.Datum.Month == selectedMonth);
            }

            return View(await events.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Event == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.ID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naam,Datum,Locatie,Beschrijving,Soort")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Event == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Naam,Datum,Locatie,Beschrijving,Soort")] Event @event)
        {
            if (id != @event.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.ID))
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
            return View(@event);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Event == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.ID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            // Markeren als verwijderd
            @event.IsDeleted = true;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}
