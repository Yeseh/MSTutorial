using AutoMapper;
using MSTutorial.UserService.Dtos;
using MSTutorial.UserService.Models;

namespace MSTutorial.UserService.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
       CreateMap<UserCreateDto, UserModel>(); 
       CreateMap<UserUpdateDto, UserModel>(); 
       CreateMap<UserModel, UserReadDto>(); 
       CreateMap<UserUpdateDto, UserModel>(); 
    }
}
