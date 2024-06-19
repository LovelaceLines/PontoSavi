using PontoSavi.Domain.Entities;

namespace PontoSavi.Application.Interfaces;

public interface ICompanyWorkShiftService
{
    Task<List<WorkShift>> GetByCompanyId(int companyId);
    Task<CompanyWorkShift> Create(CompanyWorkShift companyWorkShift);
    Task<CompanyWorkShift> Delete(CompanyWorkShift companyWorkShift);
}
