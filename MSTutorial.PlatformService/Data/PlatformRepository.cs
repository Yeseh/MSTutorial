using MSTutorial.PlatformService.Models;

namespace MSTutorial.PlatformService.Data;

public class PlatformRepository : IPlatformRepository
{
    private readonly AppDbContext _context;

    public PlatformRepository(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }

    public void CreatePlatform(PlatformModel platform)
    {
        if (platform is null) { throw new ArgumentNullException(nameof(platform)); }
        _context.Platforms.Add(platform);
    }

    public IEnumerable<PlatformModel> GetAllPlatforms()
    {
        return _context.Platforms.ToList();
    }

    public PlatformModel? GetPlatformById(int Id)
    {
        return _context.Platforms.FirstOrDefault(p => p.Id == Id);
    }

    public bool PlatformExists(PlatformModel platform)
    {
        var bExists = _context.Platforms.Any(p =>
            string.Equals(p.Name, platform.Name, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(p.Publisher, platform.Publisher, StringComparison.OrdinalIgnoreCase)
        );

        return bExists;
    }

    public PlatformModel GetPlatformById(Domain.Platform.PlatformID Id)
    {
        throw new NotImplementedException();
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() >= 0;
    }
}
