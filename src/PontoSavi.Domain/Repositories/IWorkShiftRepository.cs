using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Domain.Repositories;

public interface IWorkShiftRepository : IBaseRepository<WorkShift>
{
    Task<QueryResult<WorkShiftDTO>> Query(WorkShiftFilter filter);
    Task<bool> ExistsById(int id, int companyId);
    Task<bool> ExistsByCheckInAndCheckOut(TimeOnly checkIn, TimeOnly checkOut, int companyId);
    Task<WorkShift> GetById(int id, int companyId);
}
