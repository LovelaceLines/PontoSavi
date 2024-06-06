using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

using PontoSavi.Domain.Exceptions;

namespace PontoSavi.Infra.IOC;

public static class AuthorizationSetup
{
    public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("BaseUserRolesPolicy", policy =>
            {
                policy.RequireRole(configuration.GetSection("GlobalSettings:BaseUserRoles").Value?.Split(',') ??
                    throw new AppException("GlobalSettings:BaseUserRoles is null!", HttpStatusCode.InternalServerError));
            });

            options.AddPolicy("AdminUserRolesPolicy", policy =>
            {
                policy.RequireRole(configuration.GetSection("GlobalSettings:AdminUserRoles").Value?.Split(',') ??
                    throw new AppException("GlobalSettings:AdminUserRoles is null!", HttpStatusCode.InternalServerError));
            });

            options.AddPolicy("SuperUserRolesPolicy", policy =>
            {
                policy.RequireRole(configuration.GetSection("GlobalSettings:SuperUserRoles").Value?.Split(',') ??
                    throw new AppException("GlobalSettings:SuperUserRoles is null!", HttpStatusCode.InternalServerError));
            });
        });

        return services;
    }
}
