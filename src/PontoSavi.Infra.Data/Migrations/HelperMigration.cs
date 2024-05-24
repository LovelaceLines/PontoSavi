using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using PontoSavi.Infra.Data.Context;

namespace PontoSavi.Infra.Data.Migrations;

public class HelperMigration
{
    public HelperMigration(IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }
}