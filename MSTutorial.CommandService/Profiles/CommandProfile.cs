using AutoMapper;
using MSTutorial.CommandService.Dtos;
using MSTutorial.CommandService.Models;

namespace MSTutorial.CommandService.Profiles;

public class CommandProfile : Profile 
{
    public CommandProfile()
    {
        CreateMap<PlatformModel, PlatformReadDto>();
        CreateMap<CommandModel, CommandReadDto>();
        CreateMap<CommandCreateDto, CommandModel>();
    }
}