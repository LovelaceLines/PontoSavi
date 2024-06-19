using System.Net;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Repositories;

namespace PontoSavi.Application.Services;

public class WorkShiftService : IWorkShiftService
{
    private readonly IWorkShiftRepository _repository;
    private readonly IUserWorkShiftRepository _userWorkShiftRepository;
    private readonly ICompanyWorkShiftRepository _companyWorkShiftRepository;

    public WorkShiftService(IWorkShiftRepository repository,
        IUserWorkShiftRepository userWorkShiftRepository,
        ICompanyWorkShiftRepository companyWorkShiftRepository)
    {
        _repository = repository;
        _userWorkShiftRepository = userWorkShiftRepository;
        _companyWorkShiftRepository = companyWorkShiftRepository;
    }

    public async Task<QueryResult<WorkShift>> Query(WorkShiftFilter filter) =>
        await _repository.Query(filter);

    public async Task<WorkShift> GetById(int id, int companyId) =>
        await _repository.GetById(id, companyId);

    public async Task<WorkShift> Create(WorkShift workShift)
    {
        return await _repository.Add(workShift);
    }

    public async Task<WorkShift> Update(WorkShift workShift)
    {
        if (await _userWorkShiftRepository.ExistsById(workShift.Id, workShift.CompanyId) ||
            await _companyWorkShiftRepository.ExistsById(workShift.Id, workShift.CompanyId))
            throw new AppException("Turno de trabalho não pode ser alterado pois está sendo utilizado!", HttpStatusCode.Conflict);

        return await _repository.Update(workShift);
    }

    public async Task<WorkShift> Delete(WorkShift workShift)
    {
        if (await _userWorkShiftRepository.ExistsById(workShift.Id, workShift.CompanyId) ||
            await _companyWorkShiftRepository.ExistsById(workShift.Id, workShift.CompanyId))
            throw new AppException("Turno de trabalho não pode ser excluído pois está sendo utilizado!", HttpStatusCode.Conflict);

        return await _repository.Remove(workShift);
    }
}
