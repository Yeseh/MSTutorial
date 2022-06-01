using AutoMapper;
using MSTutorial.PlatformService.Models;
using MSTutorial.PlatformService.Dtos;

namespace MSTutorial.PlatformService.Profiles;

public class PlatformsProfile : Profile
{
    public PlatformsProfile()
    {
        CreateMap<PlatformModel, PlatformReadDto>();
        CreateMap<PlatformCreateDto, PlatformModel>();
    }     
}
