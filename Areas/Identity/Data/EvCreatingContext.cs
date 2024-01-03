using EvCreating.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EvCreating.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }

    public DbSet<EvCreating.Models.Evenement> Evenement { get; set; } = default!;

    public DbSet<Mening> Mening { get; set; } = default!;

    public DbSet<EvCreating.Models.UserBeheer> UserBeheer { get; set; } = default!;

    public DbSet<EvCreating.Models.Event> Event { get; set; } = default!;

    public DbSet<EvCreating.Models.EventComment> EventComment { get; set; } = default!;

    public DbSet<EvCreating.Models.EventEvaluation> EventEvaluation { get; set; } = default!;

    public DbSet<FAQQuestion> FAQQuestion { get; set; } = default!;

    public DbSet<FAQComment> FAQComment { get; set; } = default!;

    public DbSet<EvCreating.Models.QuizAnswer> QuizAnswer { get; set; } = default!;

    public DbSet<EvCreating.Models.Language> Language { get; set; } = default!;

    public DbSet<EvCreating.Models.Parameter> Parameter { get; set; } = default!;
    
}
public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<EvCreatingUser>
{
    public void Configure(EntityTypeBuilder<EvCreatingUser> builder)
    {
       builder.Property(u => u.FirstName).HasMaxLength(50);
       builder.Property(u => u.LastName).HasMaxLength(50);
    }
}
