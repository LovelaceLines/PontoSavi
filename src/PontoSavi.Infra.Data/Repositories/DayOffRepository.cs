using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Repositories;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Infra.Data.Context;

namespace PontoSavi.Infra.Data.Repositories;

public class DayOffRepository : BaseRepository<DayOff>, IDayOffRepository
{
    private readonly AppDbContext _context;

    public DayOffRepository(AppDbContext context) : base(context) =>
        _context = context;

    public async Task<QueryResult<DayOff>> Query(DayOffFilter filter)
    {
        var query = _context.DaysOff.AsNoTracking().AsQueryable();

        query = query.Where(d => d.TenantId == filter.TenantId);

        if (!filter.Search.IsNullOrEmpty()) query = query.Where(d => d.Description!.ToLower().Contains(filter.Search!.ToLower()));

        if (filter.Id.HasValue) query = query.Where(d => d.Id == filter.Id);
        if (filter.Date.HasValue) query = query.Where(d => d.Date == filter.Date);
        if (!filter.Description.IsNullOrEmpty()) query = query.Where(d => d.Description!.ToLower().Contains(filter.Description!.ToLower()));

        if (filter.IdDescOrderSort.HasValue)
            query = filter.IdDescOrderSort.Value ? query.OrderByDescending(d => d.Id) : query.OrderBy(d => d.Id);
        if (filter.DateDescOrderSort.HasValue)
            query = filter.DateDescOrderSort.Value ? query.OrderByDescending(d => d.Date) : query.OrderBy(d => d.Date);

        var totalCount = await query.CountAsync();

        var daysoff = await query
            .Skip(filter.PageIndex * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new QueryResult<DayOff>(daysoff, totalCount);
    }

    public async Task<bool> ExistsByDate(DateTime date, int tenantId) =>
        await _context.DaysOff.AsNoTracking().AnyAsync(d => d.Date == date && d.TenantId == tenantId);

    public async Task<bool> ExistsById(int id, int tenantId) =>
        await _context.DaysOff.AsNoTracking().AnyAsync(d => d.Id == id && d.TenantId == tenantId);

    public async Task<DayOff> GetByDate(DateTime date, int tenantId) =>
        await _context.DaysOff.AsNoTracking().FirstOrDefaultAsync(d => d.Date == date && d.TenantId == tenantId) ??
            throw new AppException("Folga não encontrada!", HttpStatusCode.NotFound);

    public async Task<DayOff> GetById(int id, int tenantId) =>
        await _context.DaysOff.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id && d.TenantId == tenantId) ??
            throw new AppException("Folga não encontrada!", HttpStatusCode.NotFound);
}
