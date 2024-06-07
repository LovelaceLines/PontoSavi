using Microsoft.EntityFrameworkCore;

using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Context;

namespace PontoSavi.Infra.Data.Repositories;

public class UserSettingsRepository : BaseRepository<UserSettings>, IUserSettingsRepository
{
    private readonly AppDbContext _context;

    public UserSettingsRepository(AppDbContext context) : base(context) =>
        _context = context;

    public async Task<bool> ExistsByUserId(int userId) =>
        await _context.UserSettings.AsNoTracking().AnyAsync(x => x.UserId == userId);

    public Task<UserSettings> GetByUserId(int userId) =>
        _context.UserSettings.AsNoTracking().FirstAsync(x => x.UserId == userId);
}
