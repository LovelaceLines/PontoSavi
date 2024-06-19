using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.IOC;

public static class AutoMapperSetup
{
    public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
    {
        var autoMapperConfig = new MapperConfiguration(configure =>
        {
            configure.CreateMap<User, UserDTO>().ReverseMap();
        });

        services.AddSingleton(autoMapperConfig.CreateMapper());

        return services;
    }
}
