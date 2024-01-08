using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EvCreating.Areas.Identity.Data;
using EvCreating.Data;
using Microsoft.AspNetCore.Mvc.Razor;
using EvCreating.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using NETCore.MailKit.Infrastructure.Internal;
using Microsoft.OpenApi.Models;

namespace EvCreating
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("EvCreatingContextConnection") ?? throw new InvalidOperationException("Connection string 'EvCreatingContextConnection' not found.");
            // Add services for restfull api
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EvCreating",
                    Version = "v1"
                });
            });
            builder.Services.AddDbContext<EvCreatingContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<EvCreatingUser>((IdentityOptions options) => options.SignIn.RequireConfirmedAccount = false)
                           .AddRoles<IdentityRole>()
                           .AddEntityFrameworkStores<EvCreatingContext>();
            builder.Services.AddLocalization(options => options.ResourcesPath = "Translations");
            builder.Services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IEmailSender, MailKitEmailSender>();

            // De volgende configuratie van de MailKit wordt toegevoegd als demonstratie, maar gebruiken we niet.
            // Deze is "overschreven" door het gebruik van de database-parameters in Globals, en ge�nitialiseerd in de data Initializer
            builder.Services.Configure<MailKitOptions>(options =>
            {
                options.Server = builder.Configuration["ExternalProviders:MailKit:SMTP:Address"];
                options.Port = Convert.ToInt32(builder.Configuration["ExternalProviders:MailKit:SMTP:Port"]);
                options.Account = builder.Configuration["ExternalProviders:MailKit:SMTP:Account"];
                options.Password = builder.Configuration["ExternalProviders:MailKit:SMTP:Password"];
                options.SenderEmail = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderEmail"];
                options.SenderName = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderName"];
                options.Security = true;  // true zet ssl or tls aan
            });




            var app = builder.Build();

            Globals.App = app;

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            //gebruik van api restfull tijdens ontwikkeling
            else 
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EvCreating v1");
                });
            }

            app.UseRouting();

            app.UseAuthorization();
            app.MapRazorPages();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            using (var scope = app.Services.CreateScope())
            {

                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<EvCreatingUser>>();
                await SeedDataService.Initialize(services, userManager);

            }
            var supportedCultures = new[] { "en-US", "fr", "nl" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);


            app.Run();
        }
    }
}
