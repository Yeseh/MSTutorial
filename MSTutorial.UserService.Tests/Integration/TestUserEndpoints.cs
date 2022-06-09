using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MSTutorial.UserService.Data;
using MSTutorial.UserService.Dtos;
using MSTutorial.UserService.Profiles;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using FluentAssertions;
using Xunit;

namespace MSTutorial.UserService.Tests.Integration;

public class TestUserEndpoints
{
    private readonly Action<IServiceCollection> _serviceOverride = services =>
    {
        //var root = new InMemoryDatabaseRoot();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddDbContext<AppDbContext>(opts =>
        {
            opts.UseInMemoryDatabase("Tests");
        });
        services.AddAutoMapper(services => services.AddProfile<UserProfile>());
    };

    public TestUserEndpoints()
    {

    }

    [Fact]
    public async void GetAllUsers_should_return_all_users()
    {
        await using var app = new MinimalApiTestApplication<Program>(_serviceOverride);
        var client = app.CreateClient();

        var res = await client.GetAsync("/");
        var resText = await res.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<IEnumerable<UserReadDto>>(resText);

        res.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().HaveCount(2);
    }
}
