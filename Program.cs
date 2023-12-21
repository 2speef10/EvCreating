using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EvCreating.Areas.Identity.Data;
using EvCreating.Data;
using Microsoft.AspNetCore.Mvc.Razor;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EvCreatingContextConnection") ?? throw new InvalidOperationException("Connection string 'EvCreatingContextConnection' not found.");

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
using (var scope = app.Services.CreateScope())
{
    
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<EvCreatingUser>>();
    await SeedDataService.Initialize(services,userManager);
    
}
var supportedCultures = new[] { "en-US", "fr", "nl" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);


app.Run();
