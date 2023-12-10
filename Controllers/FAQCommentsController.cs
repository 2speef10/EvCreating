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
    public class FAQCommentsController : Controller
    {
        private readonly EvCreatingContext _context;

        public FAQCommentsController(EvCreatingContext context)
        {
            _context = context;
        }

        // GET: FAQComments
        public async Task<IActionResult> Index()
        {
            var evCreatingContext = _context.FAQComment.Include(f => f.FAQQuestion);
            return View(await evCreatingContext.ToListAsync());
        }

        // GET: FAQComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FAQComment == null)
            {
                return NotFound();
            }

            var fAQComment = await _context.FAQComment
                .Include(f => f.FAQQuestion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fAQComment == null)
            {
                return NotFound();
            }

            return View(fAQComment);
        }

        // GET: FAQComments/Create
        public IActionResult Create()
        {
            ViewData["FAQQuestionId"] = new SelectList(_context.FAQQuestion, "Id", "Description");
            return View();
        }

        // POST: FAQComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,FAQQuestionId")] FAQComment fAQComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fAQComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FAQQuestionId"] = new SelectList(_context.FAQQuestion, "Id", "Description", fAQComment.FAQQuestionId);
            return View(fAQComment);
        }

        // GET: FAQComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FAQComment == null)
            {
                return NotFound();
            }

            var fAQComment = await _context.FAQComment.FindAsync(id);
            if (fAQComment == null)
            {
                return NotFound();
            }
            ViewData["FAQQuestionId"] = new SelectList(_context.FAQQuestion, "Id", "Description", fAQComment.FAQQuestionId);
            return View(fAQComment);
        }

        // POST: FAQComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,FAQQuestionId")] FAQComment fAQComment)
        {
            if (id != fAQComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fAQComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FAQCommentExists(fAQComment.Id))
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
            ViewData["FAQQuestionId"] = new SelectList(_context.FAQQuestion, "Id", "Description", fAQComment.FAQQuestionId);
            return View(fAQComment);
        }

        // GET: FAQComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FAQComment == null)
            {
                return NotFound();
            }

            var fAQComment = await _context.FAQComment
                .Include(f => f.FAQQuestion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fAQComment == null)
            {
                return NotFound();
            }

            return View(fAQComment);
        }

        // POST: FAQComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FAQComment == null)
            {
                return Problem("Entity set 'EvCreatingContext.FAQComment'  is null.");
            }
            var fAQComment = await _context.FAQComment.FindAsync(id);
            if (fAQComment != null)
            {
                _context.FAQComment.Remove(fAQComment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FAQCommentExists(int id)
        {
          return (_context.FAQComment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
