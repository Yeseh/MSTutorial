using Microsoft.EntityFrameworkCore;
using MSTutorial.UserService.Models;

namespace MSTutorial.UserService.Data;

public class AppDbContext: DbContext 
{
    public DbSet<UserModel> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
    {

    }
}
