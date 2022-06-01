using MSTutorial.PlatformService.Models;
using MSTutorial.Domain.Platform;

namespace MSTutorial.PlatformService.Data;

public interface IPlatformRepository
{
    bool SaveChanges();

    IEnumerable<PlatformModel> GetAllPlatforms();

    PlatformModel GetPlatformById(int Id);

    //Models.Platform GetPlatformById(Domain.Platform.PlatformID Id);

    void CreatePlatform(PlatformModel platform);

    bool PlatformExists(PlatformModel platform);
}

