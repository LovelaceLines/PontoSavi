using System.Net;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;

namespace PontoSavi.Application.Services;

public class UserSettingsService : IUserSettingsService
{
    private readonly IUserSettingsRepository _userSettingsRepository;

    public UserSettingsService(IUserSettingsRepository userSettingsRepository) =>
        _userSettingsRepository = userSettingsRepository;

    public async Task<UserSettings> GetByUserId(int userId) =>
        await _userSettingsRepository.GetByUserId(userId);

    public async Task<UserSettings> Create(UserSettings userSettings)
    {
        if (await _userSettingsRepository.ExistsByUserId(userSettings.UserId))
            throw new AppException("Usuário já possui configurações de ponto cadastradas.", HttpStatusCode.BadRequest);

        return await _userSettingsRepository.Add(userSettings);
    }

    public async Task<UserSettings> Update(UserSettings userSettings)
    {
        if (!await _userSettingsRepository.ExistsByUserId(userSettings.UserId))
            throw new AppException("Usuário não possui configurações de ponto cadastradas.", HttpStatusCode.BadRequest);

        return await _userSettingsRepository.Update(userSettings);
    }
}
