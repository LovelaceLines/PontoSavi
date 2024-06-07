using PontoSavi.Domain.Entities;

namespace PontoSavi.Application.Interfaces;

public interface IUserSettingsService
{
    Task<UserSettings> GetByUserId(int userId);
    Task<UserSettings> Create(UserSettings userSettings);
    Task<UserSettings> Update(UserSettings userSettings);
}
