using Microsoft.EntityFrameworkCore;
using MSTutorial.CommandService.Models;

namespace MSTutorial.CommandService.Data;

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
        //context.Database.Migrate();

        if (!context.Platforms.Any())
        {
            Console.Write("--> Seeding Platforms...");

            context.Platforms.AddRange(
                new PlatformModel() { ExternalId = 1,  Name = "DOTNET" },
                new PlatformModel() { ExternalId = 2,  Name = "SQL Server Express" },
                new PlatformModel() { ExternalId = 3,  Name = "Kubernetes" }
            );

            context.SaveChanges();
        }

        if (!context.Commands.Any())
        {
            Console.WriteLine("--> Seeding Commands...");

            context.Commands.AddRange(
                new CommandModel() { HowTo = "Build a project", CommandLine = "dotnet build", PlatformId = 1 },
                new CommandModel() { HowTo = "Apply a configuration", CommandLine = "kubectl apply -f <filename>", PlatformId = 3 }
            );

            context.SaveChanges();
        }

        Console.Write("--> Done Seeding...");
    }
}
