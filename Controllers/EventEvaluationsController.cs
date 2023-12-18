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
    public class EventEvaluationsController : Controller
    {
        private readonly EvCreatingContext _context;

        public EventEvaluationsController(EvCreatingContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            var activeEvents = _context.Event.Where(e => !e.IsDeleted).ToList();
            ViewData["GeselecteerdEvenementen"] = new SelectList(activeEvents, "ID", "Naam");
            return View();
        }

        public async Task<IActionResult> Index(int? selectedRating)
        {
            var eventEvaluations = _context.EventEvaluation.Include(e => e.GeselecteerdEvenement);

            if (selectedRating.HasValue)
            {
                eventEvaluations = _context.EventEvaluation
                    .Where(e => e.Waardering == selectedRating)
                    .Include(e => e.GeselecteerdEvenement);
            }

            return View(await eventEvaluations.ToListAsync());
        }

    
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EventEvaluation == null)
            {
                return NotFound();
            }

            var eventEvaluation = await _context.EventEvaluation
                .Include(e => e.GeselecteerdEvenement)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eventEvaluation == null)
            {
                return NotFound();
            }

            return View(eventEvaluation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naam,ReactieDatum,Waardering,Inhoud,GeselecteerdEvenementId")] EventEvaluation eventEvaluation)
        {
            if (ModelState.IsValid)
            {
                eventEvaluation.EventNaam = _context.Event.FirstOrDefault(e => e.ID == eventEvaluation.GeselecteerdEvenementId)?.Naam;

                _context.Add(eventEvaluation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeselecteerdEvenementen"] = new SelectList(_context.Event, "ID", "Naam");
            return View(eventEvaluation);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EventEvaluation == null)
            {
                return NotFound();
            }

            var eventEvaluation = await _context.EventEvaluation.FindAsync(id);
            if (eventEvaluation == null)
            {
                return NotFound();
            }
            ViewData["GeselecteerdEvenementen"] = new SelectList(_context.Event, "ID", "Naam", eventEvaluation.GeselecteerdEvenementId);
            return View(eventEvaluation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Naam,ReactieDatum,Waardering,Inhoud,GeselecteerdEvenementId")] EventEvaluation eventEvaluation)
        {
            if (id != eventEvaluation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventEvaluation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventEvaluationExists(eventEvaluation.ID))
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
            ViewData["GeselecteerdEvenementen"] = new SelectList(_context.Event, "ID", "Naam", eventEvaluation.GeselecteerdEvenementId);
            return View(eventEvaluation);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EventEvaluation == null)
            {
                return NotFound();
            }

            var eventEvaluation = await _context.EventEvaluation
                .Include(e => e.GeselecteerdEvenement)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eventEvaluation == null)
            {
                return NotFound();
            }

            return View(eventEvaluation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EventEvaluation == null)
            {
                return Problem("Entity set 'EvCreatingContext.EventEvaluation'  is null.");
            }
            var eventEvaluation = await _context.EventEvaluation.FindAsync(id);
            if (eventEvaluation != null)
            {
                _context.EventEvaluation.Remove(eventEvaluation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventEvaluationExists(int id)
        {
            return (_context.EventEvaluation?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
