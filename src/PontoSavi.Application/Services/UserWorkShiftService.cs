using System.Net;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;

namespace PontoSavi.Application.Services;

public class UserWorkShiftService : IUserWorkShiftService
{
    private readonly IUserWorkShiftRepository _repository;

    public UserWorkShiftService(IUserWorkShiftRepository repository) =>
        _repository = repository;

    public async Task<List<WorkShift>> GetByUserId(int userId, int tenantId) =>
        await _repository.GetWorkShiftByUserId(userId, tenantId);

    public async Task<UserWorkShift> Create(UserWorkShift userWorkShift)
    {
        if (await _repository.ExistsById(userWorkShift.UserId, userWorkShift.WorkShiftId, userWorkShift.TenantId))
            throw new AppException("Configuração de turno de trabalho já existe!", HttpStatusCode.Conflict);

        return await _repository.Add(userWorkShift);
    }

    public async Task<UserWorkShift> Delete(UserWorkShift userWorkShift)
    {
        if (!await _repository.ExistsById(userWorkShift.UserId, userWorkShift.WorkShiftId, userWorkShift.TenantId))
            throw new AppException("Configuração de turno de trabalho não encontrada!", HttpStatusCode.NotFound);

        return await _repository.Remove(userWorkShift);
    }
}
