using System.Net;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;

namespace PontoSavi.Application.Services;

public class CompanyWorkShiftService : ICompanyWorkShiftService
{
    private readonly ICompanyWorkShiftRepository _repository;

    public CompanyWorkShiftService(ICompanyWorkShiftRepository repository) =>
        _repository = repository;

    public async Task<List<WorkShift>> GetByCompanyId(int companyId) =>
        await _repository.GetWorkShiftByCompanyId(companyId);

    public async Task<CompanyWorkShift> Create(CompanyWorkShift companyWorkShift)
    {
        if (await _repository.ExistsById(companyWorkShift.WorkShiftId, companyWorkShift.TenantId))
            throw new AppException("Configuração de turno de trabalho já existe!", HttpStatusCode.BadRequest);

        return await _repository.Add(companyWorkShift);
    }

    public async Task<CompanyWorkShift> Delete(CompanyWorkShift companyWorkShift)
    {
        if (!await _repository.ExistsById(companyWorkShift.WorkShiftId, companyWorkShift.TenantId))
            throw new AppException("Configuração de turno de trabalho não encontrada!", HttpStatusCode.NotFound);

        return await _repository.Remove(companyWorkShift);
    }
}
