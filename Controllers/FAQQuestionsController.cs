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
    public class FAQQuestionsController : Controller
    {
        private readonly EvCreatingContext _context;

        public FAQQuestionsController(EvCreatingContext context)
        {
            _context = context;
        }

        // GET: FAQQuestions
        public async Task<IActionResult> Index()
        {
            return _context.FAQQuestion != null ?
                        View(await _context.FAQQuestion.ToListAsync()) :
                        Problem("Entity set 'EvCreatingContext.FAQQuestion'  is null.");
        }

        // GET: FAQQuestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FAQQuestion == null)
            {
                return NotFound();
            }

            var fAQQuestion = await _context.FAQQuestion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fAQQuestion == null)
            {
                return NotFound();
            }

            return View(fAQQuestion);
        }

        // GET: FAQQuestions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FAQQuestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] FAQQuestion fAQQuestion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fAQQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fAQQuestion);
        }

        // GET: FAQQuestions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FAQQuestion == null)
            {
                return NotFound();
            }

            var fAQQuestion = await _context.FAQQuestion.FindAsync(id);
            if (fAQQuestion == null)
            {
                return NotFound();
            }
            return View(fAQQuestion);
        }

        // POST: FAQQuestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] FAQQuestion fAQQuestion)
        {
            if (id != fAQQuestion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fAQQuestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FAQQuestionExists(fAQQuestion.Id))
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
            return View(fAQQuestion);
        }

        // GET: FAQQuestions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FAQQuestion == null)
            {
                return NotFound();
            }

            var fAQQuestion = await _context.FAQQuestion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fAQQuestion == null)
            {
                return NotFound();
            }

            return View(fAQQuestion);
        }

        // POST: FAQQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FAQQuestion == null)
            {
                return Problem("Entity set 'EvCreatingContext.FAQQuestion'  is null.");
            }
            var fAQQuestion = await _context.FAQQuestion.FindAsync(id);
            if (fAQQuestion != null)
            {
                _context.FAQQuestion.Remove(fAQQuestion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FAQQuestionExists(int id)
        {
            return (_context.FAQQuestion?.Any(e => e.Id == id)).GetValueOrDefault();

        }
    }
}