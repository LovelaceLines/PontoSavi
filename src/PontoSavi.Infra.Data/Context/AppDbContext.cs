using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using PontoSavi.Domain.Entities;
using PontoSavi.Infra.Data.Configurations;

namespace PontoSavi.Infra.Data.Context;

public class AppDbContext : IdentityDbContext<User, Role, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Company> Companies { get; set; }
    public override DbSet<User> Users { get; set; }
    public override DbSet<Role> Roles { get; set; }
    public override DbSet<UserRole> UserRoles { get; set; }
    public DbSet<DayOff> DaysOff { get; set; }
    public DbSet<Point> Points { get; set; }
    public DbSet<WorkShift> WorkShifts { get; set; }
    public DbSet<UserWorkShift> UserWorkShifts { get; set; }
    public DbSet<CompanyWorkShift> CompanyWorkShifts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new RoleClaimConfiguration());
        builder.ApplyConfiguration(new UserClaimConfiguration());
        builder.ApplyConfiguration(new UserLoginConfiguration());
        builder.ApplyConfiguration(new UserTokenConfiguration());

        builder.ApplyConfiguration(new CompanyConfiguration());
        builder.ApplyConfiguration(new WorkShifts());

        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserSettingsConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());

        builder.ApplyConfiguration(new DayOffConfiguration());
        builder.ApplyConfiguration(new PointConfiguration());
        builder.ApplyConfiguration(new UserWorkShiftConfiguration());
        builder.ApplyConfiguration(new CompanyWorkShiftConfiguration());
    }
}
