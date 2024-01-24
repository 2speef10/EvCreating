using EvCreating.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

public class EventAnalyticsMiddleware
{
    private readonly RequestDelegate _next;

    public EventAnalyticsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var eventId = context.Request.Query["eventId"];
        LogEventVisit(context, eventId);

        await _next(context);

        stopwatch.Stop();
        var elapsedTime = stopwatch.ElapsedMilliseconds;

        // LogVisitDuration(context, eventId, elapsedTime); // Verwijder deze regel

        // Roep LogVisitCount aan om de Visit Count te loggen
        LogVisitCount(context, eventId);
    }

    private void LogEventVisit(HttpContext context, string eventId)
    {
        Console.WriteLine($"Gebruiker bezocht evenement met ID {eventId}");

        // Opslaan in HttpContext.Items
        var eventVisitsKey = $"EventVisits_{eventId}";
        var eventVisits = context.Items[eventVisitsKey] as List<string> ?? new List<string>();
        eventVisits.Add($"Gebruiker bezocht evenement met ID {eventId}");
        context.Items[eventVisitsKey] = eventVisits;

        // Logging toevoegen
        Console.WriteLine($"Event Visits in HttpContext.Items ({eventId}):");
        foreach (var visit in eventVisits)
        {
            Console.WriteLine(visit);
        }
    }

    private void LogVisitCount(HttpContext context, string eventId)
    {
        Console.WriteLine($"Gebruiker bezocht evenement met ID {eventId}");

        // Opslaan in HttpContext.Items
        var visitCountKey = $"VisitCount_{eventId}";
        var visitCount = context.Items[visitCountKey] as int? ?? 0;
        visitCount++;
        context.Items[visitCountKey] = visitCount;

        // Logging toevoegen
        Console.WriteLine($"Event Visit Count in HttpContext.Items ({eventId}): {visitCount}");

        // Zorg ervoor dat de visitCount als string wordt opgeslagen
        context.Items[$"VisitCountString_{eventId}"] = visitCount.ToString();
    }

}
