using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using EvCreating.Data;
using EvCreating.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json; // Voeg deze namespace toe voor JSON-serialisatie
using EvCreating.Middleware;

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

            // Voeg een nieuw dictionary toe om het aantal bezoeken per evenement bij te houden
            var visitCounts = new Dictionary<int, int>();

            foreach (var @event in await events.ToListAsync())
            {
                // Haal gegevens op uit HttpContext.Items voor een specifiek evenement
                var eventVisitsKey = $"EventVisits_{@event.ID}";

                var eventVisits = HttpContext.Items[eventVisitsKey] as List<string> ?? new List<string>();

                // Voeg het aantal bezoeken toe aan de dictionary
                visitCounts[@event.ID] = eventVisits.Count;
            }

            // Voeg het dictionary met het aantal bezoeken toe aan ViewBag
            ViewBag.VisitCounts = visitCounts;

            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
            return View(await events.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FirstOrDefaultAsync(m => m.ID == id);

            if (@event == null)
            {
                return NotFound();
            }

            // Roep LogEventVisit aan om het bezoek aan het evenement te loggen
            //LogEventVisit(HttpContext, @event.ID.ToString());

            // Haal gegevens op uit HttpContext.Items voor een specifiek evenement
            var eventVisitsKey = $"EventVisits_{@event.ID}";
            var eventVisits = HttpContext.Items[eventVisitsKey] as List<string> ?? new List<string>();

            // Voeg de VisitCount toe aan het model
            var visitCountKey = $"VisitCount_{@event.ID}";
            var visitCount = HttpContext.Items[visitCountKey] as int? ?? 0;

            // Geef de gegevens door aan de view
            ViewBag.EventVisits = eventVisits;
            ViewBag.VisitCount = visitCount;

            // Voeg het model toe aan HttpContext.Items
            HttpContext.Items["EventModel"] = @event;

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
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(@event);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Evenement succesvol gecreëerd.";
                    return RedirectToAction(nameof(Index));
                }

                return View(@event);
            }
            catch (Exception ex)
            {
                // Log de fout, voeg toe aan een foutenlog of stuur naar een monitoringsservice
                Console.Error.WriteLine($"Fout bij het maken van het evenement: {ex.Message}");

                // Geef het foutbericht aan de gebruiker
                ModelState.AddModelError(string.Empty, "Er is een fout opgetreden bij het maken van het evenement.");

                return View(@event);
            }
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
                catch (DbUpdateConcurrencyException ex)
                {
                    // Lokaliseer foutbericht op basis van de huidige taal
                    string errorMessage = LocalizeErrorMessage(ex);

                    // Verwerk de foutmelding, bijvoorbeeld, loggen of weergeven aan de gebruiker
                    Console.WriteLine(errorMessage);

                    return View(@event);
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

        private string LocalizeErrorMessage(DbUpdateConcurrencyException ex)
        {
            // Je moet hier logica toevoegen om het foutbericht te lokaliseren
            // op basis van de huidige taal van de applicatie.
            // Dit kan worden gedaan met behulp van ASP.NET Core's ingebouwde lokaliseringsmechanisme.
            // Hier is een vereenvoudigd voorbeeld, je moet je eigen implementatie toevoegen.

            // Huidige taal verkrijgen (bijvoorbeeld uit de gebruikersinstellingen)
            var currentLanguage = GetCurrentLanguage();

            // Simpele vertalingen (je zou dit uit een externe bron kunnen halen, zoals .resx-bestanden)
            var translations = new Dictionary<string, Dictionary<string, string>>
            {
                {"en", new Dictionary<string, string> {{"Event not found", "Event not found"}}},
                {"nl", new Dictionary<string, string> {{"Event not found", "Evenement niet gevonden"}}}
                // Voeg andere vertalingen toe zoals nodig
            };

            // Krijg de foutmelding uit de uitzondering
            var errorMessage = ex.Message;

            // Probeer de fout te vertalen
            if (translations.ContainsKey(currentLanguage) && translations[currentLanguage].ContainsKey(errorMessage))
            {
                return translations[currentLanguage][errorMessage];
            }

            // Gebruik de standaardfout als er geen vertaling beschikbaar is
            return errorMessage;
        }

        private string GetCurrentLanguage()
        {
            // Implementeer logica om de huidige taal van de applicatie te bepalen
            // Dit kan bijvoorbeeld afkomstig zijn van de gebruikersinstellingen of de huidige threadcultuur.
            // Hier is een vereenvoudigd voorbeeld.
            return "nl"; // Geef de standaardtaal terug als voorbeeld
        }
        public IActionResult EventDetails(int eventId)
        {
            // Haal gegevens op uit HttpContext.Items
            var eventVisits = HttpContext.Items[$"EventVisits_{eventId}"] as List<string> ?? new List<string>();
            // var visitDurations = HttpContext.Items[$"VisitDurations_{eventId}"] as Dictionary<string, long> ?? new Dictionary<string, long>();

            // Geef de gegevens door aan de view
            ViewBag.EventVisits = eventVisits;
            // ViewBag.VisitDurations = visitDurations;

            // Voeg een extra eigenschap toe voor het aantal bezoeken
            ViewBag.VisitCount = eventVisits.Count;

            return View();
        }

    }
}
