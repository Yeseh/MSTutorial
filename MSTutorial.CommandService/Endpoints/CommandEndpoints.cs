using AutoMapper;
using MSTutorial.CommandService.Data;
using MSTutorial.CommandService.Dtos;
using MSTutorial.CommandService.Models;

namespace MSTutorial.CommandService.Endpoints;

public static class CommandEndpoints
{
   private const string PATH = "/api/c/platforms";
   private const string PLATFORM_ID_PATH = PATH + "/{platformId:int}";
   private const string COMMAND_PATH = PLATFORM_ID_PATH + "/commands";
   private const string COMMAND_ID_PATH = COMMAND_PATH + "/{commandId:int}";

   public static void MapCommandEndpoints(this WebApplication app) 
   { 
      app.MapGet(COMMAND_PATH, GetCommandsForPlatform).WithName(nameof(GetCommandsForPlatform));
      app.MapPost(COMMAND_PATH, CreateCommand).WithName(nameof(CreateCommand));
      app.MapGet(COMMAND_ID_PATH, GetCommand).WithName(nameof(GetCommand));
   }
    
   private static IResult GetCommandsForPlatform(int platformId, ICommandRepository _repo, IMapper _mapper)
   {
        var bExists = _repo.PlatformExists(platformId);
        if (!bExists) { return Results.NotFound(); }

        var models = _repo.GetCommandsForPlatform(platformId);
        var result = _mapper.Map<IEnumerable<CommandReadDto>>(models);

        return Results.Ok(result);
   }
   
   private static IResult GetCommand(int platformId, int commandId,  ICommandRepository _repo, IMapper _mapper)
   {
        var bExists = _repo.PlatformExists(platformId);
        if (!bExists) { return Results.NotFound(); }

        var model = _repo.GetCommand(platformId, commandId);
        if (model is null) { return Results.NotFound(); }

        var result = _mapper.Map<CommandReadDto>(model);
        return Results.Ok(result);
   }

   private static IResult CreateCommand(
        int platformId, 
        CommandCreateDto command, 
        ICommandRepository _repo, 
        IMapper _mapper)
   {
        var bExists = _repo.PlatformExists(platformId);
        if (!bExists) { return Results.NotFound(); }

        var model = _mapper.Map<CommandModel>(command);
        _repo.CreateCommand(platformId, model);

        var created = _repo.SaveChanges();
        if (!created) { return Results.BadRequest(); }

        var result = _mapper.Map<CommandReadDto>(model);
        var route = new { platformId = result.PlatformId, Id = result.Id };
        return Results.CreatedAtRoute(nameof(CreateCommand), route, result);
   }
}
