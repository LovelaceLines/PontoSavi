using System.Net;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Repositories;

namespace PontoSavi.Application.Services;

public class DayOffService : IDayOffService
{
    private readonly IDayOffRepository _repository;

    public DayOffService(IDayOffRepository repository) =>
        _repository = repository;

    public async Task<QueryResult<DayOff>> Query(DayOffFilter filter) =>
        await _repository.Query(filter);

    public async Task<DayOff> GetById(int id, int tenantId) =>
        await _repository.GetById(id, tenantId);

    public async Task<DayOff> GetByDate(DateTime date, int tenantId) =>
        await _repository.GetByDate(date, tenantId);

    public async Task<DayOff> Create(DayOff dayOff)
    {
        if (await _repository.ExistsByDate(dayOff.Date, dayOff.TenantId))
            throw new AppException("Já existe uma folga cadastrada para esta data!", HttpStatusCode.BadRequest);

        return await _repository.Add(dayOff);
    }

    public async Task<DayOff> Update(DayOff newDayOff)
    {
        var oldDayOff = await _repository.GetById(newDayOff.Id);

        if (oldDayOff.Date != newDayOff.Date && await _repository.ExistsByDate(newDayOff.Date, oldDayOff.TenantId))
            throw new AppException("Já existe uma folga cadastrada para esta data!", HttpStatusCode.BadRequest);

        oldDayOff.Date = newDayOff.Date;
        oldDayOff.Description = newDayOff.Description;

        return await _repository.Update(oldDayOff);
    }

    public async Task<DayOff> Delete(DayOff dayOff) =>
        await _repository.Remove(dayOff);
}
