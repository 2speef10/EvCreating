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
    public class QuizController : Controller
    {
        private readonly EvCreatingContext _context;

        public QuizController(EvCreatingContext context)
        {
            _context = context;
        }

        // ... (Eerdere acties voor het ophalen van vragen en het controleren van antwoorden)

        // GET: QuizAnswers
        public async Task<IActionResult> Index()
        {
            return _context.QuizAnswer != null ?
                View(await _context.QuizAnswer.ToListAsync()) :
                Problem("Entity set 'EvCreatingContext.QuizAnswer'  is null.");
        }

        // GET: QuizAnswers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.QuizAnswer == null)
            {
                return NotFound();
            }

            var quizAnswer = await _context.QuizAnswer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizAnswer == null)
            {
                return NotFound();
            }

            return View(quizAnswer);
        }

        // GET: QuizAnswers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuizAnswers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserAnswer")] QuizAnswer quizAnswer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quizAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quizAnswer);
        }

        // GET: QuizAnswers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.QuizAnswer == null)
            {
                return NotFound();
            }

            var quizAnswer = await _context.QuizAnswer.FindAsync(id);
            if (quizAnswer == null)
            {
                return NotFound();
            }
            return View(quizAnswer);
        }

        // POST: QuizAnswers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserAnswer")] QuizAnswer quizAnswer)
        {
            if (id != quizAnswer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quizAnswer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizAnswerExists(quizAnswer.Id))
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
            return View(quizAnswer);
        }

        // GET: QuizAnswers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.QuizAnswer == null)
            {
                return NotFound();
            }

            var quizAnswer = await _context.QuizAnswer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizAnswer == null)
            {
                return NotFound();
            }

            return View(quizAnswer);
        }

        // POST: QuizAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.QuizAnswer == null)
            {
                return Problem("Entity set 'EvCreatingContext.QuizAnswer'  is null.");
            }
            var quizAnswer = await _context.QuizAnswer.FindAsync(id);
            if (quizAnswer != null)
            {
                _context.QuizAnswer.Remove(quizAnswer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizAnswerExists(int id)
        {
            return (_context.QuizAnswer?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // POST: Quiz/CheckAnswers
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckAnswers(string UserAnswer1, string UserAnswer2, string UserAnswer3)
        {
            var quizAnswers = new List<QuizAnswer>
            {
                new QuizAnswer { UserAnswer = UserAnswer1 },
                new QuizAnswer { UserAnswer = UserAnswer2 },
                new QuizAnswer { UserAnswer = UserAnswer3 }
            };

            try
            {
                _context.QuizAnswer.AddRange(quizAnswers);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem($"Er is een fout opgetreden: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
