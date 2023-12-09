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
    public class EventCommentsController : Controller
    {
        private readonly EvCreatingContext _context;

        public EventCommentsController(EvCreatingContext context)
        {
            _context = context;
        }

        // GET: EventComments
        public async Task<IActionResult> Index()
        {
            var evCreatingContext = _context.EventComment.Include(e => e.Event);
            return View(await evCreatingContext.ToListAsync());
        }

        // GET: EventComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EventComment == null)
            {
                return NotFound();
            }

            var eventComment = await _context.EventComment
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eventComment == null)
            {
                return NotFound();
            }

            return View(eventComment);
        }

        // GET: EventComments/Create
        public IActionResult Create()
        {
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "ID");
            return View();
        }

        // POST: EventComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserName,ReactieDatum,Waardering,Inhoud,EventID")] EventComment eventComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "ID", eventComment.EventID);
            return View(eventComment);
        }

        // GET: EventComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EventComment == null)
            {
                return NotFound();
            }

            var eventComment = await _context.EventComment.FindAsync(id);
            if (eventComment == null)
            {
                return NotFound();
            }
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "ID", eventComment.EventID);
            return View(eventComment);
        }

        // POST: EventComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserName,ReactieDatum,Waardering,Inhoud,EventID")] EventComment eventComment)
        {
            if (id != eventComment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventCommentExists(eventComment.ID))
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
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "ID", eventComment.EventID);
            return View(eventComment);
        }

        // GET: EventComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EventComment == null)
            {
                return NotFound();
            }

            var eventComment = await _context.EventComment
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eventComment == null)
            {
                return NotFound();
            }

            return View(eventComment);
        }

        // POST: EventComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EventComment == null)
            {
                return Problem("Entity set 'EvCreatingContext.EventComment'  is null.");
            }
            var eventComment = await _context.EventComment.FindAsync(id);
            if (eventComment != null)
            {
                _context.EventComment.Remove(eventComment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventCommentExists(int id)
        {
          return (_context.EventComment?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
