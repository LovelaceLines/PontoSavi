using Microsoft.EntityFrameworkCore;
using System.Net;

using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Context;

namespace PontoSavi.Infra.Data.Repositories;

public class WorkShiftRepository : BaseRepository<WorkShift>, IWorkShiftRepository
{
    private readonly AppDbContext _context;

    public WorkShiftRepository(AppDbContext context) : base(context) =>
        _context = context;

    public async Task<QueryResult<WorkShift>> Query(WorkShiftFilter filter)
    {
        var query = _context.WorkShifts.AsQueryable();

        query = query.Where(x => x.CompanyId == filter.CompanyId);

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

        var totalCount = query.Count();

        var workShifts = await query
            .Skip(filter.PageIndex * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new QueryResult<WorkShift>(workShifts, totalCount);
    }

    public async Task<bool> ExistsById(int id, int companyId) =>
        await _context.WorkShifts.AsNoTracking().AnyAsync(x => x.Id == id && x.CompanyId == companyId);

    public async Task<WorkShift> GetById(int id, int companyId) =>
        await _context.WorkShifts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.CompanyId == companyId) ??
            throw new AppException("Turno de trabalho não encontrado!", HttpStatusCode.NotFound);
}
