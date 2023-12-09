using EvCreating.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EvCreating.Models;

namespace EvCreating.Data;

public class EvCreatingContext : IdentityDbContext<EvCreatingUser>
{
    public EvCreatingContext(DbContextOptions<EvCreatingContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<EvCreating.Models.Evenement> Evenement { get; set; } = default!;

    public DbSet<Mening> Mening { get; set; } = default!;

    public DbSet<EvCreating.Models.UserBeheer> UserBeheer { get; set; } = default!;

    public DbSet<EvCreating.Models.Event> Event { get; set; } = default!;

    public DbSet<EvCreating.Models.EventComment> EventComment { get; set; } = default!;

    public DbSet<EvCreating.Models.EventEvaluation> EventEvaluation { get; set; } = default!;
}
