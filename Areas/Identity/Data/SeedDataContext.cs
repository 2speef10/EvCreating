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
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<EvCreatingUser>userManager)

        {

            using (var context = new EvCreatingContext(
                serviceProvider.GetRequiredService<DbContextOptions<EvCreatingContext>>()))
            {
                // Controleer of er al gegevens zijn toegevoegd
                if (context.Event.FirstOrDefault(e => e.Naam == "Evenement 1") == null || context.Event.FirstOrDefault(e => e.Naam == "Evenement 2") == null)
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
                if (context.FAQComment.Any())
                {
                    context.FAQComment.AddRange(
                      new FAQComment { Content = "Comment 1" },
                      new FAQComment { Content = "Comment 2" }

                                                                                           );

                }
                context.SaveChanges();
                
                if (!context.Users.Any())
                {
                    EvCreatingUser user = new EvCreatingUser
                    {
                        Id = "User",
                        UserName = "User",
                        FirstName = "User",
                        LastName = "User",
                        Email = "User@user.com",
                        PasswordHash = "User123."

                    };

                    context.Users.Add(user);
                    context.SaveChanges();

                    EvCreatingUser admin = new EvCreatingUser

                    {
                        Id = "Admin",
                        UserName = "Admin",
                        FirstName = "Admin",
                        LastName = "Admin",
                        Email = "ilias.filali23@gmail.com"

                    };
                    var result = await userManager.CreateAsync(admin, "Admin123.");




                }

                EvCreatingUser dummyUser = context.Users.FirstOrDefault(g => g.UserName == "User");
                EvCreatingUser dummyAdmin = context.Users.FirstOrDefault(g => g.UserName == "Admin");
                AddParameters(context, dummyAdmin);
                Globals.GlobalsUser = dummyUser;

                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new IdentityRole
                        {
                            Id = "User",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new IdentityRole
                        {
                            Id = "SystemAdministrator",
                            Name = "SystemAdministrator",
                            NormalizedName = "SYSTEMADMINISTRATOR"

                        });
                    context.UserRoles.Add(
                        new IdentityUserRole<string>
                        {
                            UserId = dummyUser.Id,
                            RoleId = "User"
                        });
                    context.UserRoles.Add(
                         new IdentityUserRole<string>
                         {
                             UserId = dummyAdmin.Id,
                             RoleId = "SystemAdministrator"
                         });
                    context.SaveChanges();
                }


                if (!context.Language.Any())
                {
                    context.AddRange(
                        new Language { Id = "-", Name = "-", IsSystemLanguage = false, IsAvailable = DateTime.MaxValue },
                        new Language { Id = "nl", Name = "Nederlands", IsSystemLanguage = true },
                        new Language { Id = "en", Name = "English", IsSystemLanguage = true },
                        new Language { Id = "fr", Name = "Français", IsSystemLanguage = true }
                        
                        );
                    context.SaveChanges();
                }

                Language.GetLanguages(context);


                
                if (!context.EventEvaluation.Any())
                {
                    context.EventEvaluation.AddRange(
                    new EventEvaluation { Naam = "Ilias", ReactieDatum = DateTime.Now, Waardering = 4, Inhoud = "Leuke Event", EventNaam = evenement2.Naam, GeselecteerdEvenementId = evenement2.ID,EvCreatingUserId=dummyAdmin.Id,EvCreatingUser = dummyAdmin},
                    new EventEvaluation { Naam = "Kamil", ReactieDatum = DateTime.Now, Waardering = 2, Inhoud = "niet interesant", EventNaam = evenement.Naam, GeselecteerdEvenementId = evenement.ID, EvCreatingUser=dummyUser, EvCreatingUserId = dummyUser.Id }
                                                                                                                                                            );

                }
                context.SaveChanges();
                
            }
            static void AddParameters(EvCreatingContext context, EvCreatingUser user)
            {
                if (!context.Parameter.Any())
                {
                    context.Parameter.AddRange(
                        new Parameter { Name = "Version", Value = "0.1.0", Description = "Huidige versie van de parameterlijst", Destination = "System", UserId = user.Id },
                        new Parameter { Name = "Mail.Server", Value = "ergens.evcreating.be", Description = "Naam van de gebruikte pop-server", Destination = "Mail", UserId = user.Id },
                        new Parameter { Name = "Mail.Port", Value = "25", Description = "Poort van de smtp-server", Destination = "Mail", UserId = user.Id },
                        new Parameter { Name = "Mail.Account", Value = "SmtpServer", Description = "Acount-naam van de smtp-server", Destination = "Mail", UserId = user.Id },
                        new Parameter { Name = "Mail.Password", Value = "xxxyyy!2315", Description = "Wachtwoord van de smtp-server", Destination = "Mail", UserId = user.Id },
                        new Parameter { Name = "Mail.Security", Value = "true", Description = "Is SSL or TLS encryption used (true or false)", Destination = "Mail", UserId = user.Id },
                        new Parameter { Name = "Mail.SenderEmail", Value = "administrator.evcreating.be", Description = "E-mail van de smtp-verzender", Destination = "Mail", UserId = user.Id },
                        new Parameter { Name = "Mail.SenderName", Value = "Administrator", Description = "Naam van de smtp-verzender", Destination = "Mail", UserId = user.Id }
                    );
                    context.SaveChanges();
                }

                Globals.Parameters = new Dictionary<string, Parameter>();
                foreach (Parameter parameter in context.Parameter)
                {
                    Globals.Parameters[parameter.Name] = parameter;
                }
                Globals.ConfigureMail();
            }

        }
        }
    }


