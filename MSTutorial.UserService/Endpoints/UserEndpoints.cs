using AutoMapper;
using MSTutorial.UserService.Data;
using MSTutorial.UserService.Dtos;
using MSTutorial.UserService.Models;

namespace MSTutorial.UserService.Endpoints;

public static class UserEndpoints
{
    public const string PATH = "/api/users";
    public const string PATH_WITH_ID = PATH + "/{userId:int}";
    
    public static void MapUserEndpoints(WebApplication app)
    {
        app.MapPost(PATH, CreateUser).WithName(nameof(CreateUser));
        app.MapGet(PATH, GetAllUsers).WithName(nameof(GetAllUsers));
        app.MapGet(PATH_WITH_ID, GetUser).WithName(nameof(GetUser));
        app.MapPut(PATH_WITH_ID, UpdateUser).WithName(nameof(UpdateUser));
        app.MapDelete(PATH_WITH_ID, DeleteUser).WithName(nameof(DeleteUser));
    }

    public static IResult GetAllUsers(IUserRepository _repo, IMapper _mapper)
    {
        var models = _repo.GetAllUsers();
        var result = _mapper.Map<IEnumerable<UserReadDto>>(models);

        return Results.Ok(result);
    }

    public static IResult GetUser(int userId, IUserRepository _repo, IMapper _mapper)
    {
        var model = _repo.GetUser(userId);
        if (model is null) { return Results.NotFound(); }

        var result = _mapper.Map<UserReadDto>(model);

        return Results.Ok(result);
    }

    public static IResult CreateUser(UserCreateDto dto, IUserRepository _repo, IMapper _mapper)
    {
        var bExists = _repo.UserEmailExists(dto.EmailAddress);
        if (bExists) { return Results.Conflict(); }

        var model = _mapper.Map<UserModel>(dto);
        var created = _repo.CreateUser(model);
        if (!created) { return Results.BadRequest(); }

        var readDto = _mapper.Map<UserReadDto>(model);
        var route = new { Id = model.Id };
        
        // PublishNewUser()

        return Results.CreatedAtRoute(nameof(CreateUser), route, readDto);
    }

    public static IResult DeleteUser(int userId, IUserRepository _repo, IMapper _mapper)
    {
        var deleted = _repo.DeleteUser(userId);
        if (!deleted) { return Results.NotFound(); }

        return Results.NoContent();
    }

    public static IResult UpdateUser(UserUpdateDto dto, IUserRepository _repo, IMapper _mapper)
    {
        var model = _mapper.Map<UserModel>(dto);
        var updated = _repo.UpdateUser(model);
        if (!updated) { return Results.NotFound(); }

        var readDto = _mapper.Map<UserReadDto>(model);
        return Results.Ok(readDto);
    }
}
