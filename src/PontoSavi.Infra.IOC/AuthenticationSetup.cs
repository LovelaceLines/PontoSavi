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
                ValidIssuer = configuration.GetSection("Jwt:Issuer").Value ?? throw new AppException("Jwt: Issuer is null", HttpStatusCode.InternalServerError),
                ValidAudience = configuration.GetSection("Jwt:Audience").Value ?? throw new AppException("Jwt: Audience is null", HttpStatusCode.InternalServerError),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:SecretKey").Value ?? throw new AppException("Jwt: Secret is null", HttpStatusCode.InternalServerError))),
            };
        });

        return services;
    }
}
