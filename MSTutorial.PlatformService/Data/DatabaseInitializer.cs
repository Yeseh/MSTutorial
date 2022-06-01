using MSTutorial.PlatformService.Models;

namespace MSTutorial.PlatformService.Data;

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
       if (!context.Platforms.Any())
       {
            Console.Write("--> Seeding Data...");
            context.Platforms.AddRange(
                new PlatformModel() { Name = "DOTNET", Publisher = "Microsoft", Cost = "Free" },
                new PlatformModel() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                new PlatformModel() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
            );

            context.SaveChanges();
       }

       Console.Write("--> Done Seeding...");
    }
}
