using Microsoft.EntityFrameworkCore;
using MSTutorial.CommandService.Data;
using MSTutorial.CommandService.DataServices;
using MSTutorial.CommandService.Endpoints;
using MSTutorial.CommandService.EventProcessing;

var builder = WebApplication.CreateBuilder(args);
var sqlconnStr = builder.Configuration.GetConnectionString("CommandsConnectionString");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(sqlconnStr));
builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("InMemory"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICommandRepository, CommandRepository>();

builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddHostedService<MessageBusSubscriber>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPlatformEndpoints();
app.MapCommandEndpoints();

DatabaseInitializer.Initialize(app);

app.Run();
