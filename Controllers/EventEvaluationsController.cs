using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvCreating.Data;
using EvCreating.Models;
using EvCreating.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace EvCreating.Controllers
{
    public class EventEvaluationsController : Controller
    {
        private readonly UserManager<EvCreatingUser> _userManager;
        private readonly EvCreatingContext _context;

        public EventEvaluationsController(EvCreatingContext context, UserManager<EvCreatingUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Create()
        {
            var activeEvents = _context.Event.Where(e => !e.IsDeleted).ToList();
            ViewData["GeselecteerdEvenementen"] = new SelectList(activeEvents, "ID", "Naam");
            return View();
        }

        public async Task<IActionResult> Index(int? selectedRating)
        {
            var eventEvaluations = _context.EventEvaluation.Include(e => e.GeselecteerdEvenement).Include(e => e.EvCreatingUser);
            

            if (selectedRating.HasValue)
            {
                eventEvaluations = _context.EventEvaluation
                    .Where(e => e.Waardering == selectedRating)
                    .Include(e => e.GeselecteerdEvenement).Include(e => e.EvCreatingUser);
            }

            // Loop door elke EventEvaluation en vul EvCreatingUser in
            foreach (var evaluation in await eventEvaluations.ToListAsync())
            {
                // Vervang 'GetUserById' met de juiste methode om de gebruiker op te halen op basis van EvCreatingUserId
                var user = await GetUserById(evaluation.EvCreatingUserId);

                // Voeg de gebruikersnaam toe aan EvCreatingUser als de gebruiker is gevonden
                if (user != null)
                {
                    evaluation.EvCreatingUser = user; // Of de juiste eigenschap die de gebruikersnaam bevat
                }
            }

            return View(await eventEvaluations.ToListAsync());
        }
        // Methode om de gebruiker op te halen op basis van EvCreatingUserId
        private async Task<EvCreatingUser> GetUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
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

                // Set EvCreatingUserId to the current logged-in user's ID
                var currentUser = await _userManager.GetUserAsync(User);
                eventEvaluation.EvCreatingUserId = currentUser.Id;

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
