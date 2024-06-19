using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Context;

namespace PontoSavi.Infra.Data.Repositories;

public class PointRepository : BaseRepository<Point>, IPointRepository
{
    private readonly AppDbContext _context;

    public PointRepository(AppDbContext context) : base(context) =>
        _context = context;

    public async Task<QueryResult<Point>> Query(PointFilter filter)
    {
        var query = _context.Points.AsQueryable();

        query = query.Where(p => p.CompanyId == filter.CompanyId);

        if (!filter.Search.IsNullOrEmpty())
            query = query.Where(d =>
                d.CheckInDescription!.ToLower().Contains(filter.Search!.ToLower()) ||
                d.CheckOutDescription!.ToLower().Contains(filter.Search!.ToLower()));

        if (filter.Id.HasValue) query = query.Where(p => p.Id == filter.Id);
        if (filter.UserId.HasValue) query = query.Where(p => p.UserId == filter.UserId);
        if (filter.ManagerId.HasValue) query = query.Where(p => p.ManagerId == filter.ManagerId);
        if (filter.CheckIn.HasValue) query = query.Where(p => p.CheckIn >= filter.CheckIn);
        if (filter.CheckInStatus.HasValue) query = query.Where(p => p.CheckInStatus == filter.CheckInStatus);
        if (filter.CheckOut.HasValue) query = query.Where(p => p.CheckOut <= filter.CheckOut);
        if (filter.CheckOutStatus.HasValue) query = query.Where(p => p.CheckOutStatus == filter.CheckOutStatus);

        if (filter.IdDescOrderSort.HasValue) query = filter.IdDescOrderSort.Value ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id);
        if (filter.CheckInDescOrderSort.HasValue) query = filter.CheckInDescOrderSort.Value ? query.OrderByDescending(p => p.CheckIn) : query.OrderBy(p => p.CheckIn);
        if (filter.CheckOutDescOrderSort.HasValue) query = filter.CheckOutDescOrderSort.Value ? query.OrderByDescending(p => p.CheckOut) : query.OrderBy(p => p.CheckOut);

        var totalCount = await query.CountAsync();

        var points = await query
            .Skip(filter.PageIndex * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new QueryResult<Point>(points, totalCount);
    }

    public async Task<bool> ExistsById(int id, int companyId) =>
        await _context.Points.AsNoTracking().AnyAsync(p => p.Id == id && p.CompanyId == companyId);

    public async Task<bool> ExistsOpenPointByUserId(int userId, int companyId) =>
        await _context.Points.AsNoTracking().AnyAsync(p => p.UserId == userId && p.CompanyId == companyId && p.CheckOut == null);

    public async Task<Point> GetById(int id, int companyId) =>
        await _context.Points.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id && p.CompanyId == companyId) ??
            throw new AppException("Ponto não encontrado!", HttpStatusCode.NotFound);

    public async Task<Point> GetOpenPointByUserId(int userId, int companyId) =>
        await _context.Points.AsNoTracking().FirstOrDefaultAsync(p => p.UserId == userId && p.CompanyId == companyId && p.CheckOut == null) ??
            throw new AppException("Nenhum ponto aberto encontrado!", HttpStatusCode.NotFound);
}
