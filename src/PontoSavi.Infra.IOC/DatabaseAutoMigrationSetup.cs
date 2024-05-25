using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using PontoSavi.Infra.Data.Context;

namespace PontoSavi.Infra.IOC;

public static class DatabaseAutoMigrationSetup
{
    public static IHost AddDatabaseAutoMigrationConfiguration(this IHost host)
    {
        var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<object>>();
        var dbContext = services.GetRequiredService<AppDbContext>();

        try
        {
            dbContext.Database.Migrate();
            logger.LogInformation("Database migration completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database.");
            throw;
        }

        return host;
    }
}
