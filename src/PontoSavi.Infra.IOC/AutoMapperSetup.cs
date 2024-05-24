using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using PontoSavi.Domain.DTOs;

namespace PontoSavi.Infra.IOC;

public static class AutoMapperSetup
{
    public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
    {
        var autoMapperConfig = new MapperConfiguration(configure =>
        {
            configure.CreateMap<UserDTO, IdentityUser>().ReverseMap();
            configure.CreateMap<RoleDTO, IdentityRole>().ReverseMap();
        });

        services.AddSingleton(autoMapperConfig.CreateMapper());

        return services;
    }
}
