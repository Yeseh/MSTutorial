using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSTutorial.PlatformService.Data;
using MSTutorial.PlatformService.DataServices.Http;
using MSTutorial.PlatformService.Dtos;
using MSTutorial.PlatformService.Models;

namespace MSTutorial.PlatformService.Endpoints;

public static class PlatformEndpoints
{
    public const string PATH = "/";
    public const string PATH_WITH_ID = PATH + "{id:int}";

    public static void MapPlatformEndpoints(this WebApplication app)
    {
        app.MapGet(PATH, GetAllPlatforms).WithName(nameof(GetAllPlatforms));
        app.MapGet(PATH_WITH_ID, GetPlatformById).WithName(nameof(GetPlatformById));
        app.MapPost(PATH, CreatePlatform).WithName(nameof(CreatePlatform));
    }
    
    private static IResult GetAllPlatforms(IPlatformRepository repo, IMapper mapper)
    {
        var result = repo.GetAllPlatforms();
        var platformDtos = mapper.Map<IEnumerable<PlatformReadDto>>(result);

        return Results.Ok(platformDtos);
    }

    private static IResult GetPlatformById(int id, IPlatformRepository repo, IMapper mapper)
    {
        var platform = repo.GetPlatformById(id);
        var result = mapper.Map<PlatformReadDto>(platform);

        if (result != null) { return Results.Ok(result); }

        return Results.NotFound();
    }

    private static async Task<IResult> CreatePlatform(
        PlatformCreateDto platform, 
        IPlatformRepository repo, 
        IMapper mapper, 
        ICommandDataClient cmdClient)
    {
        var model = mapper.Map<PlatformModel>(platform);
        var exists = repo.PlatformExists(model);
        if (exists) { return Results.Conflict(); }

        repo.CreatePlatform(model);
        var created = repo.SaveChanges();
        if (!created) { return Results.BadRequest(); }

        var readDto = mapper.Map<PlatformReadDto>(model);
        try { await cmdClient.SendPlatformToCommand(readDto); }
        catch (Exception ex) { Console.WriteLine($"--> Could not send synchronously: {ex.Message}"); }

        return Results.CreatedAtRoute(nameof(GetPlatformById), new { Id = readDto.Id }, readDto);
    }
}
