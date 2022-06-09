using Microsoft.EntityFrameworkCore;
using MSTutorial.UserService.Models;

namespace MSTutorial.UserService.Data;

public class DatabaseInitializer
{
    private readonly AppDbContext _context;
    private static UserModel[] _users = new UserModel[]
    {
        new UserModel() { FamilyName = "Wellenberg", GivenName = "Jesse", EmailAddress = "dev@jessewellenberg.nl" },
        new UserModel() { FamilyName = "Randy", GivenName = "Wind", EmailAddress = "randywind@nativedevelopment.com" }
    };

    public DatabaseInitializer(AppDbContext context)
    {
        _context = context;
    }

    public void Initialize()
    {
        _context.Users.AddRange(_users);
        _context.SaveChanges();
    }

    public static void Initialize(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetService<AppDbContext>();

        if (context == null) { throw new Exception($"{nameof(AppDbContext)} service is missing!"); }
        
        SeedData(context);
    }
    
    private static void SeedData(AppDbContext context)
    {
       context.Database.Migrate();
       if (!context.Users.Any())
       {
            Console.Write("--> Seeding Data...");
            context.Users.AddRange(_users);
            context.SaveChanges();
       }
       Console.Write("--> Done Seeding...");
    }

}