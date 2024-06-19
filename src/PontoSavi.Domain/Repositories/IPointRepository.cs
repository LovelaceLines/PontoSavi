using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Domain.Repositories;

public interface IPointRepository : IBaseRepository<Point>
{
    Task<QueryResult<Point>> Query(PointFilter filter);
    Task<bool> ExistsById(int id, int companyId);
    Task<bool> ExistsOpenPointByUserId(int userId, int companyId);
    Task<Point> GetById(int id, int companyId);
    Task<Point> GetOpenPointByUserId(int userId, int companyId);
}
