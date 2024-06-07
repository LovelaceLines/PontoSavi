using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PontoSavi.Domain.Entities;
using PontoSavi.Infra.Data.Configurations;

namespace PontoSavi.Infra.Data.Context;

public class AppDbContext : IdentityDbContext<User, Role, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public override DbSet<User> Users { get; set; }
    public DbSet<UserSettings> UserSettings { get; set; }
    public override DbSet<Role> Roles { get; set; }
    public DbSet<Company> Companies { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserSettingsConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());

        builder.ApplyConfiguration(new CompanyConfiguration());
    }
}
