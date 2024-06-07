using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PontoSavi.API.ViewModels;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.IOC;

public static class AutoMapperSetup
{
    public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
    {
        var autoMapperConfig = new MapperConfiguration(configure =>
        {
            configure.CreateMap<UserDTO, User>().ReverseMap();
            configure.CreateMap<UserSettings, UserSettingsVM>().ReverseMap();
            configure.CreateMap<RoleDTO, Role>().ReverseMap();

            configure.CreateMap<CompanyDTO, Company>().ReverseMap();
        });

        services.AddSingleton(autoMapperConfig.CreateMapper());

        return services;
    }
}
