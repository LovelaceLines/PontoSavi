using PontoSavi.Domain.Entities;

namespace PontoSavi.Domain.Repositories;

public interface ICompanyWorkShiftRepository : IBaseRepository<CompanyWorkShift>
{
    Task<bool> ExistsById(int workShiftId, int tenantId);
    Task<CompanyWorkShift> GetById(int workShiftId, int tenantId);
    Task<List<WorkShift>> GetWorkShiftByCompanyId(int companyId);
}
