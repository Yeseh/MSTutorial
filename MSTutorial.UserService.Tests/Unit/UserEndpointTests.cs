using Moq;
using Xunit;
using MSTutorial.UserService.Endpoints;
using MSTutorial.UserService.Data;
using MSTutorial.UserService.Profiles;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using MSTutorial.UserService.Models;
using System.Collections.Generic;
using MinimalApis.Extensions.Results;
using System.Linq;

namespace MSTutorial.UserService.Tests;

internal static class UserFixtures
{
    public readonly static UserModel User1 = new()
    {
        Id = 1,
        GivenName = "Jesse",
        FamilyName = "Wellenberg",
        EmailAddress = "dev@jessewellenberg.nl",
    };
    
    public readonly static UserModel User2 = new()
    {
        Id = 2,
        GivenName = "Randy",
        FamilyName = "Wind",
        EmailAddress = "randywind@nativedevelopment.com",
    };

    public readonly static List<UserModel> UserList = new() { User1, User2 };
}


public class UserEndpointTests 
{
    private readonly Mock<IUserRepository> _repo;
    private readonly IMapper _mapper;
    
    public UserEndpointTests() 
    {
        _repo = new Mock<IUserRepository>();

        var mapperConfig = new MapperConfiguration(c => c.AddProfile<UserProfile>());
        _mapper = mapperConfig.CreateMapper();
    }

    //[Fact]
    //public void GetAllUsers_returns_ok_status() 
    //{
    //    _repo.Setup(r => r.GetAllUsers()).Returns(UserFixtures.UserList);
    //    var result = UserEndpoints.GetAllUsers(_repo.Object, _mapper)!;
    //    result.
    //        Assert.NotNull(result.Value);

    //    var value = (List<UserModel>)result.Value!;
        
    //    Assert.Equal(200, result.StatusCode);
    //    Assert.Equal(2, value.Count);
    //    Assert.Equal(1, value[0].Id);
    //}
}