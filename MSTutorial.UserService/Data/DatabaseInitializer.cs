using Microsoft.EntityFrameworkCore;
using MSTutorial.UserService.Models;

namespace MSTutorial.UserService.Data;

public static class DatabaseInitializer
{
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
            context.Users.AddRange(
                new UserModel() { FamilyName = "Wellenberg", GivenName = "Jesse", EmailAddress = "dev@jessewellenberg.nl" }
            );

            context.SaveChanges();
       }

       Console.Write("--> Done Seeding...");
    }
}