using MSTutorial.PlatformService.Dtos;

namespace MSTutorial.PlatformService.DataServices.MessageBus;

public interface IMessageBusClient
{
    void PublishNewPlatform(PlatformPublishedDto platformPublished);
}