using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Application.Interfaces;

public interface IPointService
{
    Task<QueryResult<Point>> Query(PointFilter filter);
    Task<Point> GetById(int id, int tenantId);
    Task<Point> GetOpenPoint(int userId, int tenantId);
    Task<Point> ManualCheckIn(Point point);
    Task<Point> ManualCheckOut(Point point);
    Task<Point> AutoCheckIn(int userId, int tenantId, string? description);
    Task<Point> AutoCheckOut(int userId, int tenantId, string? description);
    Task<Point> UpdateDescription(Point point);
    Task<Point> UpdateFull(Point point);
    Task<Point> Approve(int id, int managerId, int tenantId);
    Task<Point> Reject(int id, int managerId, int tenantId);
}
