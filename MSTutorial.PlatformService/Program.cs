using Microsoft.EntityFrameworkCore;
using MSTutorial.PlatformService.Data;
using MSTutorial.PlatformService.Endpoints;
using MSTutorial.PlatformService.DataServices.Http;
using MSTutorial.PlatformService.DataServices.MessageBus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(opts =>
    {
        opts.UseInMemoryDatabase("Platforms");
    });
}
else
{
    var connString = builder.Configuration.GetConnectionString("PlatformsConnectionString");
    builder.Services.AddDbContext<AppDbContext>(opts =>
    {
        opts.UseSqlServer(connString);
    });
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPlatformEndpoints();
DatabaseInitializer.Initialize(app, app.Environment.IsProduction());

var cmdUrl = builder.Configuration["CommandServiceUrl"];
Console.WriteLine($"Commandservice configured at {cmdUrl}");

app.Run();
