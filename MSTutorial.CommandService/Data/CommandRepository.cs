using MSTutorial.CommandService.Data;
using MSTutorial.CommandService.Models;

public class CommandRepository : ICommandRepository
{
    private readonly AppDbContext _context;

    public CommandRepository(AppDbContext context)
    {
       _context = context;
    }

    public void CreateCommand(int platformId, CommandModel command)
    {
        if (command is null) { throw new ArgumentNullException(nameof(command)); }

        command.PlatformId = platformId;

        _context.Commands.Add(command);
        _context.SaveChanges();
    }

    public void CreatePlatform(PlatformModel platform)
    {
        if (platform is null) { throw new ArgumentNullException(nameof(platform)); }

        _context.Add(platform);
        _context.SaveChanges();
    }

    public IEnumerable<PlatformModel> GetAllPlatforms()
    {
        return _context.Platforms.ToList();
    }

    public CommandModel? GetCommand(int platformId, int commandId)
    {
        return _context.Commands
            .Where(c => c.PlatformId == platformId && c.Id == commandId)
            .FirstOrDefault();
    }

    public IEnumerable<CommandModel> GetCommandsForPlatform(int platformId)
    {
        return _context.Commands.Where(c => c.PlatformId == platformId);
    }

    public bool PlatformExists(int platformId)
    {
        return _context.Platforms.Any(p => p.Id == platformId);
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() >= 0;
    }
}