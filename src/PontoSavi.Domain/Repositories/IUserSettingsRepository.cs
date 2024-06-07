using PontoSavi.Domain.Entities;

namespace PontoSavi.Domain.Repositories;

public interface IUserSettingsRepository : IBaseRepository<UserSettings>
{
    Task<bool> ExistsByUserId(int userId);
    Task<UserSettings> GetByUserId(int userId);
}
