using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using MSTutorial.UserService.Models;
using MSTutorial.UserService.Data;
using MSTutorial.UserService.Profiles;
using Microsoft.EntityFrameworkCore;
using System;

namespace MSTutorial.UserService.Tests;

// TODO: Make generic version for Yeseh.Utils


class MinimalApiTestApplication<T> : WebApplicationFactory<UserModel>
{
    private readonly Action<IServiceCollection> _serviceOverride;

    public MinimalApiTestApplication(Action<IServiceCollection> serviceOverride)
    {
        _serviceOverride = serviceOverride;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(_serviceOverride);
        base.Services.GetRequiredService<DatabaseInitializer>().Initialize();

        return base.CreateHost(builder);
    }
}
