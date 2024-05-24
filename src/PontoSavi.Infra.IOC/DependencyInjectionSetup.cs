using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

using PontoSavi.API.ServiceFilters;
using PontoSavi.Application.Interfaces;
using PontoSavi.Application.Services;
using PontoSavi.Application.Validators;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Repositories;

namespace PontoSavi.Infra.IOC;

public static class DependencyInjectionSetup
{
    public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);

        services.AddTransient<AuthAndUserExtractionFilter>();

        services.AddTransient<IUserService, UserService>();
        services.AddTransient<UserValidator>();
        services.AddTransient<PasswordValidator>();
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddTransient<IdentityUserRole<string>>();
        services.AddTransient<SignInManager<IdentityUser>>();

        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IRoleRepository, RoleRepository>();

        services.AddTransient<IUserRoleRepository, UserRoleRepository>();
        services.AddTransient<IUserRoleService, UserRoleService>();

        services.AddTransient<IAuthService, AuthService>();

        return services;
    }
}
