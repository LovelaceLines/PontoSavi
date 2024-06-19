using PontoSavi.Domain.Entities;

namespace PontoSavi.Domain.Repositories;

public interface IUserWorkShiftRepository : IBaseRepository<UserWorkShift>
{
    Task<bool> ExistsById(int workShiftId, int companyId);
    Task<bool> ExistsById(int userId, int workShiftId, int companyId);
    Task<bool> ExistsByUserId(int userId, int companyId);
    Task<UserWorkShift> GetById(int userId, int workShiftId, int companyId);
    Task<List<WorkShift>> GetWorkShiftByUserId(int userId, int companyId);
}
