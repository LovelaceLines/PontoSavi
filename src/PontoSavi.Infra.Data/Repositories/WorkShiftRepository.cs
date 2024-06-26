using Microsoft.EntityFrameworkCore;
using System.Net;

using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Context;
using PontoSavi.Domain.DTOs;

namespace PontoSavi.Infra.Data.Repositories;

public class WorkShiftRepository : BaseRepository<WorkShift>, IWorkShiftRepository
{
    private readonly AppDbContext _context;

    public WorkShiftRepository(AppDbContext context) : base(context) =>
        _context = context;

    public async Task<QueryResult<WorkShiftDTO>> Query(WorkShiftFilter filter)
    {
        var query = _context.WorkShifts.AsQueryable();

        query = query.Where(x => x.TenantId == filter.TenantId);

        if (filter.Id.HasValue) query = query.Where(x => x.Id == filter.Id);
        if (filter.CheckIn.HasValue) query = query.Where(x => x.CheckIn == filter.CheckIn);
        if (filter.CheckInToleranceMinutes.HasValue) query = query.Where(x => x.CheckInToleranceMinutes == filter.CheckInToleranceMinutes);
        if (filter.CheckOut.HasValue) query = query.Where(x => x.CheckOut == filter.CheckOut);
        if (filter.CheckOutToleranceMinutes.HasValue) query = query.Where(x => x.CheckOutToleranceMinutes == filter.CheckOutToleranceMinutes);

        if (filter.IdDescOrderSort.HasValue)
            query = filter.IdDescOrderSort.Value ? query.OrderByDescending(x => x.Id) : query.OrderBy(x => x.Id);
        if (filter.CheckInDescOrderSort.HasValue)
            query = filter.CheckInDescOrderSort.Value ? query.OrderByDescending(x => x.CheckIn) : query.OrderBy(x => x.CheckIn);
        if (filter.CheckOutDescOrderSort.HasValue)
            query = filter.CheckOutDescOrderSort.Value ? query.OrderByDescending(x => x.CheckOut) : query.OrderBy(x => x.CheckOut);

        var _query = query
            .GroupJoin(_context.UserWorkShifts, ws => ws.Id, uws => uws.WorkShiftId, (ws, uws) => new { ws, uws })
            .SelectMany(x => x.uws.DefaultIfEmpty(), (ws, uws) => new { ws.ws, uws })
            .GroupJoin(_context.Users, wsuws => wsuws.uws!.UserId, u => u.Id, (wsuws, u) => new { wsuws.ws, u })
            .SelectMany(x => x.u.DefaultIfEmpty(), (ws, u) => new { ws.ws, u })
            .GroupJoin(_context.CompanyWorkShifts, wsu => wsu.ws.Id, cws => cws.WorkShiftId, (wsu, cws) => new { wsu.ws, wsu.u, cws })
            .SelectMany(x => x.cws.DefaultIfEmpty(), (wsu, cws) => new { wsu.ws, wsu.u, cws })
            .GroupJoin(_context.Companies, wsucws => wsucws.cws!.TenantId, c => c.Id, (wsucws, c) => new { wsucws.ws, wsucws.u, c })
            .SelectMany(x => x.c.DefaultIfEmpty(), (wsuc, c) => new { wsuc.ws, wsuc.u, c });

        var totalCount = _query.Count();

        var workShifts = await _query
            .Skip(filter.PageIndex * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => new WorkShiftDTO(x.ws, x.u, x.c))
            .ToListAsync();

        return new QueryResult<WorkShiftDTO>(workShifts, totalCount);
    }

    public async Task<bool> ExistsById(int id, int tenantId) =>
        await _context.WorkShifts.AsNoTracking().AnyAsync(x => x.Id == id && x.TenantId == tenantId);

    public async Task<bool> ExistsByCheckInAndCheckOut(TimeOnly checkIn, TimeOnly checkOut, int tenantId) =>
        await _context.WorkShifts.AsNoTracking().AnyAsync(x => x.CheckIn == checkIn && x.CheckOut == checkOut && x.TenantId == tenantId);

    public async Task<WorkShift> GetById(int id, int tenantId) =>
        await _context.WorkShifts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.TenantId == tenantId) ??
            throw new AppException("Turno de trabalho não encontrado!", HttpStatusCode.NotFound);
}
