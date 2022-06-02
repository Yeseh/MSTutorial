using System.Text.Json;
using AutoMapper;
using MSTutorial.CommandService.Data;
using MSTutorial.CommandService.Dtos;
using MSTutorial.CommandService.Models;

namespace MSTutorial.CommandService.EventProcessing;

public enum EventType
{
    Null = -1,
    Undetermined,
    PlatformPublished,
}

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _mapper = mapper;
    }

    public void ProcessEvent(string message)
    {
        var eventType = DetermineEvent(message);
        switch (eventType) 
        {
            case EventType.PlatformPublished:
                AddPlatform(message);
                break;
            default:
                break;
        }
    }

    private EventType DetermineEvent(string notificationMessage)
    {
        Console.WriteLine("--> Determining event");
        var msg = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
        var eventType = EventType.Null;

        switch(msg.Event)
        {
            case "Platform_Published":
                eventType = EventType.PlatformPublished;
                break;
            default:
                eventType = EventType.Undetermined;
                break;
        }

        return eventType;
    }

    private void AddPlatform(string platformPublishedMessage)
    {
        using var scope = _scopeFactory.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<ICommandRepository>();
        var dto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

        try
        {
            var platform = _mapper.Map<PlatformModel>(dto);
            var bExists = repo.ExternalPlatformExists(platform.ExternalId);
            if (!bExists) 
            {
                repo.CreatePlatform(platform);
                repo.SaveChanges();
                Console.WriteLine("--> Platform added");
            }
            else 
            {
                Console.WriteLine("--> Platform already exists...");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Error creating platform {ex.Message}");
        }
    }
}
