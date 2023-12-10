﻿using EvCreating.Models;
using EvCreating.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace EvCreating.Data
{
    public class SeedDataService
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EvCreatingContext(
                serviceProvider.GetRequiredService<DbContextOptions<EvCreatingContext>>()))
            {
                // Controleer of er al gegevens zijn toegevoegd
                if (context.Event.FirstOrDefault(e=>e.Naam== "Evenement 1")==null|| context.Event.FirstOrDefault(e => e.Naam == "Evenement 2") == null)
                {

                    context.Event.AddRange(
                        new Event { Naam = "Evenement 1", Datum = DateTime.Now, Locatie = "Locatie 1", Beschrijving = "Beschrijving 1", Soort = "Soort 1" },
                        new Event { Naam = "Evenement 2", Datum = DateTime.Now.AddDays(7), Locatie = "Locatie 2", Beschrijving = "Beschrijving 2", Soort = "Soort 2" }
                        // Voeg meer evenementen toe zoals hierboven
                     
                    );
                }
                context.SaveChanges();
                Event evenement = context.Event.FirstOrDefault(e => e.Naam == "Evenement 1");
                Event evenement2 = context.Event.FirstOrDefault(e => e.Naam == "Evenement 2");
                if (!context.FAQQuestion.Any())
                {
                    context.FAQQuestion.AddRange(
                    new FAQQuestion { Title = "Eerste vraag", Description = "Answer 1" }, 
                    new FAQQuestion { Title = "tweede vraag", Description = "Answer 2" }
                                                                                         );

                }   
                context.SaveChanges();
                if (context.FAQComment.Any()) {
                    context.FAQComment.AddRange(
                      new FAQComment { Content = "Comment 1" },
                      new FAQComment { Content = "Comment 2" }
                      
                                                                                           );

                }
                context.SaveChanges();
                if (!context.EventEvaluation.Any())
                {
                    context.EventEvaluation.AddRange(
                    new EventEvaluation { Naam = "Ilias", ReactieDatum = DateTime.Now, Waardering = 4, Inhoud = "Leuke Event", GeselecteerdEvenement = evenement2, GeselecteerdEvenementId = evenement2.ID },
                    new EventEvaluation { Naam = "Kamil", ReactieDatum = DateTime.Now, Waardering = 2, Inhoud = "niet interesant", GeselecteerdEvenement = evenement,GeselecteerdEvenementId = evenement.ID }
                                                                                                                                                            ) ;

                }   
                context.SaveChanges();
            }
        }
            
    }
}
