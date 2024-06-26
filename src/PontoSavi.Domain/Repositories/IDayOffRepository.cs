using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Domain.Repositories;

public interface IDayOffRepository : IBaseRepository<DayOff>
{
    Task<QueryResult<DayOff>> Query(DayOffFilter filter);
    Task<bool> ExistsById(int id, int tenantId);
    Task<bool> ExistsByDate(DateTime date, int tenantId);
    Task<DayOff> GetById(int id, int tenantId);
    Task<DayOff> GetByDate(DateTime date, int tenantId);
}
