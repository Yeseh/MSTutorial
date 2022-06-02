using MSTutorial.CommandService.Models;

namespace MSTutorial.CommandService.Data;

public interface ICommandRepository
{
    bool SaveChanges();

    // Platforms
    IEnumerable<PlatformModel> GetAllPlatforms();
    bool CreatePlatform(PlatformModel platform);
    bool PlatformExists(int platformId);
    bool ExternalPlatformExists(int externalPlatformId);

    // Commands
    IEnumerable<CommandModel> GetCommandsForPlatform(int platformId);
    CommandModel GetCommand(int platformId, int commandId);
    bool CreateCommand(int platformId, CommandModel command);
}