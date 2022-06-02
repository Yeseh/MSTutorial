using Microsoft.EntityFrameworkCore;
using MSTutorial.PlatformService.Models;

namespace MSTutorial.PlatformService.Data;

public class AppDbContext : DbContext
{
    public DbSet<PlatformModel> Platforms { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
    {

    }
}
