using System.Net;
using Microsoft.EntityFrameworkCore;

using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Context;

namespace PontoSavi.Infra.Data.Repositories;

public class CompanyWorkShiftRepository : BaseRepository<CompanyWorkShift>, ICompanyWorkShiftRepository
{
    private readonly AppDbContext _context;

    public CompanyWorkShiftRepository(AppDbContext context) : base(context) =>
        _context = context;

    public async Task<bool> ExistsById(int workShiftId, int companyId) =>
        await _context.CompanyWorkShifts.AsNoTracking()
            .AnyAsync(x => x.WorkShiftId == workShiftId && x.CompanyId == companyId);

    public async Task<CompanyWorkShift> GetById(int workShiftId, int companyId) =>
        await _context.CompanyWorkShifts.AsNoTracking()
            .FirstOrDefaultAsync(x => x.WorkShiftId == workShiftId && x.CompanyId == companyId) ??
                throw new AppException("Configuração de turno de trabalho não encontrada!", HttpStatusCode.NotFound);

    public async Task<List<WorkShift>> GetWorkShiftByCompanyId(int companyId) =>
        await _context.CompanyWorkShifts.AsNoTracking()
            .Where(x => x.CompanyId == companyId)
            .Join(_context.WorkShifts, x => x.WorkShift, y => y, (x, y) => y)
            .Select(x => x)
            .ToListAsync();
}
