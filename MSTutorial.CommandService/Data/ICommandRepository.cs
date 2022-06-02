using MSTutorial.CommandService.Models;

namespace MSTutorial.CommandService.Data;

public interface ICommandRepository
{
    bool SaveChanges();

    // Platforms
    IEnumerable<PlatformModel> GetAllPlatforms();
    void CreatePlatform(PlatformModel platform);
    bool PlatformExists(int platformId);

    // Commands
    IEnumerable<CommandModel> GetCommandsForPlatform(int platformId);
    CommandModel GetCommand(int platformId, int commandId);
    void CreateCommand(int platformId, CommandModel command);
}