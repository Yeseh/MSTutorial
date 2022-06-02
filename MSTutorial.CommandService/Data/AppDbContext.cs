using Microsoft.EntityFrameworkCore;
using MSTutorial.CommandService.Models;

namespace MSTutorial.CommandService.Data;

public class AppDbContext : DbContext
{
    public DbSet<PlatformModel> Platforms { get; set; }
    public DbSet<CommandModel> Commands { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlatformModel>()
                    .HasMany(p => p.Commands)
                    .WithOne(p => p.Platform!)
                    .HasForeignKey(p => p.PlatformId);

        modelBuilder.Entity<CommandModel>()
                    .HasOne(p => p.Platform)
                    .WithMany(p => p.Commands)
                    .HasForeignKey(p => p.PlatformId);
    }
}
