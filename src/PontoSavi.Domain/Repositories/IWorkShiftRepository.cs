using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Domain.Repositories;

public interface IWorkShiftRepository : IBaseRepository<WorkShift>
{
    Task<QueryResult<WorkShift>> Query(WorkShiftFilter filter);
    Task<bool> ExistsById(int id, int companyId);
    Task<WorkShift> GetById(int id, int companyId);
}
