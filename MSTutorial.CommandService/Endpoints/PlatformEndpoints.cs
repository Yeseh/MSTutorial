using AutoMapper;
using MSTutorial.CommandService.Data;
using MSTutorial.CommandService.Dtos;

namespace MSTutorial.CommandService.Endpoints;

public static class PlatformEndpoints
{
    private const string PATH = "/api/c/platforms";
    private const string PATH_WITH_ID = PATH + "/{id:int}";

    public static void MapPlatformEndpoints(this WebApplication app)
    {
        app.MapGet(PATH, GetAllPlatforms).WithName(nameof(GetAllPlatforms));
    }

    private static IResult GetAllPlatforms(ICommandRepository _repo, IMapper _mapper)
    {
        var models = _repo.GetAllPlatforms();
        var result = _mapper.Map<IEnumerable<PlatformReadDto>>(models);

        return Results.Ok(result);
    }
}
