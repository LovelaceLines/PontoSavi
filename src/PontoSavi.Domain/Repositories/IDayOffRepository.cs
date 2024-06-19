using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Domain.Repositories;

public interface IDayOffRepository : IBaseRepository<DayOff>
{
    Task<QueryResult<DayOff>> Query(DayOffFilter filter);
    Task<bool> ExistsById(int id, int companyId);
    Task<bool> ExistsByDate(DateTime date, int companyId);
    Task<DayOff> GetById(int id, int companyId);
    Task<DayOff> GetByDate(DateTime date, int companyId);
}
