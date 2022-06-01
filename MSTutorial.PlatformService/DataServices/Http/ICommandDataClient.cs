using MSTutorial.PlatformService.Dtos;

namespace MSTutorial.PlatformService.DataServices.Http;

public interface ICommandDataClient
{
    Task SendPlatformToCommand(PlatformReadDto platform);
}
