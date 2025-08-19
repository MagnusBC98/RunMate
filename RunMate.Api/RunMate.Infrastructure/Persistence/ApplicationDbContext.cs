using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RunMate.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using RunMate.Domain.Entities;

namespace RunMate.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public DbSet<RunningStats> RunningStats { get; set; }
    public DbSet<Run> Runs { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Ignore<User>();

        builder.Entity<RunningStats>()
            .HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<RunningStats>(rs => rs.UserId);

        builder.Entity<Run>()
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(r => r.UserId);
    }
}