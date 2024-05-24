using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace PontoSavi.Infra.IOC;

public static class LoggerSetup
{
    public static IServiceCollection AddLoggerConfiguration(this IServiceCollection services, IConfiguration configuration, ILoggingBuilder loggingBuilder)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        loggingBuilder.AddSerilog();

        return services;
    }
}
