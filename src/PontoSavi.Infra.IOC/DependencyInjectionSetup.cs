using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

using PontoSavi.API.ServiceFilters;
using PontoSavi.Application.Interfaces;
using PontoSavi.Application.Services;
using PontoSavi.Application.Validators;
using PontoSavi.Domain.Constants;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Repositories;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.IOC;

public static class DependencyInjectionSetup
{
    public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);

        services.AddSingleton<IRolesSettingsService, RolesSettingsService>();

        services.AddTransient<AuthAndUserExtractionFilter>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<UserValidator>();
        services.AddScoped<PasswordValidator>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IdentityUserRole<int>>();
        services.AddScoped<SignInManager<User>>();
        services.AddScoped<UserManager<User>>();
        services.AddScoped<RoleManager<Role>>();

        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IUserRoleService, UserRoleService>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        services.AddScoped<IDayOffService, DayOffService>();
        services.AddScoped<IDayOffRepository, DayOffRepository>();

        services.AddScoped<IPointService, PointService>();
        services.AddScoped<IPointRepository, PointRepository>();

        services.AddScoped<IWorkShiftService, WorkShiftService>();
        services.AddScoped<IWorkShiftRepository, WorkShiftRepository>();

        services.AddScoped<IUserWorkShiftRepository, UserWorkShiftRepository>();
        services.AddScoped<IUserWorkShiftService, UserWorkShiftService>();

        services.AddScoped<ICompanyWorkShiftRepository, CompanyWorkShiftRepository>();
        services.AddScoped<ICompanyWorkShiftService, CompanyWorkShiftService>();

        return services;
    }
}
