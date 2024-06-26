using System.Net;
using Microsoft.EntityFrameworkCore;

using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Context;

namespace PontoSavi.Infra.Data.Repositories;

public class UserWorkShiftRepository : BaseRepository<UserWorkShift>, IUserWorkShiftRepository
{
    private readonly AppDbContext _context;

    public UserWorkShiftRepository(AppDbContext context) : base(context) =>
        _context = context;

    public async Task<bool> ExistsById(int workShiftId, int tenantId) =>
        await _context.UserWorkShifts.AsNoTracking()
            .AnyAsync(x => x.WorkShiftId == workShiftId && x.TenantId == tenantId);

    public async Task<bool> ExistsById(int userId, int workShiftId, int tenantId) =>
        await _context.UserWorkShifts.AsNoTracking()
            .AnyAsync(x => x.UserId == userId && x.WorkShiftId == workShiftId && x.TenantId == tenantId);

    public async Task<bool> ExistsByUserId(int userId, int tenantId) =>
        await _context.UserWorkShifts.AsNoTracking()
            .AnyAsync(x => x.UserId == userId && x.TenantId == tenantId);

    public async Task<UserWorkShift> GetById(int userId, int workShiftId, int tenantId) =>
        await _context.UserWorkShifts.AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId && x.WorkShiftId == workShiftId && x.TenantId == tenantId) ??
                throw new AppException("Configuração de turno de trabalho não encontrada!", HttpStatusCode.NotFound);

    public async Task<List<WorkShift>> GetWorkShiftByUserId(int userId, int tenantId) =>
        await _context.UserWorkShifts.AsNoTracking()
            .Where(x => x.UserId == userId && x.TenantId == tenantId)
            .Join(_context.WorkShifts, x => x.WorkShift, y => y, (x, y) => y)
            .Select(x => x)
            .ToListAsync();
}
