using EvCreating.Models;
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
                if (!context.Event.Any())
                {
                    context.Event.AddRange(
                        new Event { Naam = "Evenement 1", Datum = DateTime.Now, Locatie = "Locatie 1", Beschrijving = "Beschrijving 1", Soort = "Soort 1" },
                        new Event { Naam = "Evenement 2", Datum = DateTime.Now.AddDays(7), Locatie = "Locatie 2", Beschrijving = "Beschrijving 2", Soort = "Soort 2" }
                        // Voeg meer evenementen toe zoals hierboven
                     
                    );
                }
                context.SaveChanges();
                if (!context.FAQQuestion.Any())
                {
                    context.FAQQuestion.AddRange(
                    new FAQQuestion { Title = "Question 1", Description = "Answer 1" }, 
                    new FAQQuestion { Title = "Question 2", Description = "Answer 2" }
                                                                                         );

                }   
                context.SaveChanges();
                if (context.FAQComment.Any()) {
                    context.FAQComment.AddRange(
                      new FAQComment { Content = "Comment 1" },
                      new FAQComment { Content = "Comment 2" }
                      
                                                                                           );

                }
            }
        }
            
    }
}
