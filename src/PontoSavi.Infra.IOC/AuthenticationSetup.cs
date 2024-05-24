using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

using PontoSavi.Domain.Exceptions;

namespace PontoSavi.Infra.IOC;

public static class AuthenticationSetup
{
    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Issuer"] ?? throw new AppException("JwtConfig: Issuer is null", HttpStatusCode.InternalServerError),
                ValidAudience = configuration["Audience"] ?? throw new AppException("JwtConfig: Audience is null", HttpStatusCode.InternalServerError),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"] ?? throw new AppException("JwtConfig: Secret is null", HttpStatusCode.InternalServerError))),
            };
        });

        return services;
    }
}
