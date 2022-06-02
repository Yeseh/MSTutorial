namespace MSTutorial.CommandService.Endpoints;

public static class PlatformEndpoints
{
    private const string PATH = "/api/c";
    private const string PATH_WITH_ID = PATH + "/{id:int}";

    public static void MapPlatformEndpoints(this WebApplication app)
    {
        app.MapPost(PATH + "/platforms", TestInboundConnection).WithName(nameof(TestInboundConnection));
    }

    private static IResult TestInboundConnection()
    {
        Console.WriteLine("--> CmdService Received connection");
        return Results.NoContent();
    }
}
