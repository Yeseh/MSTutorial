using Microsoft.EntityFrameworkCore;
using MSTutorial.PlatformService.Data;
using MSTutorial.PlatformService.Endpoints;
using MSTutorial.PlatformService.DataServices.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

builder.Services.AddDbContext<AppDbContext>(opts =>
{
    opts.UseInMemoryDatabase("Platforms");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPlatformEndpoints();
DatabaseInitializer.Initialize(app);

var cmdUrl = builder.Configuration["CommandServiceUrl"];
Console.WriteLine($"Commandservice configured at {cmdUrl}");

app.Run();
