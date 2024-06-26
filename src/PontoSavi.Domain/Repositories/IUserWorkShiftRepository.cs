using PontoSavi.Domain.Entities;

namespace PontoSavi.Domain.Repositories;

public interface IUserWorkShiftRepository : IBaseRepository<UserWorkShift>
{
    Task<bool> ExistsById(int workShiftId, int tenantId);
    Task<bool> ExistsById(int userId, int workShiftId, int tenantId);
    Task<bool> ExistsByUserId(int userId, int tenantId);
    Task<UserWorkShift> GetById(int userId, int workShiftId, int tenantId);
    Task<List<WorkShift>> GetWorkShiftByUserId(int userId, int tenantId);
}
