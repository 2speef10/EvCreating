using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EvCreating.Areas.Identity.Data;
using EvCreating.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EvCreatingContextConnection") ?? throw new InvalidOperationException("Connection string 'EvCreatingContextConnection' not found.");

builder.Services.AddDbContext<EvCreatingContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<EvCreatingUser>((IdentityOptions options) => options.SignIn.RequireConfirmedAccount = true)
               .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<EvCreatingContext>();
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
    SeedDataService.Initialize(services);
}

app.Run();
