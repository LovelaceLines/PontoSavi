using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Application.Interfaces;

public interface IWorkShiftService
{
    Task<QueryResult<WorkShiftDTO>> Query(WorkShiftFilter filter);
    Task<WorkShift> GetById(int id, int tenantId);
    Task<WorkShift> Create(WorkShift workShift);
    Task<WorkShift> Update(WorkShift workShift);
    Task<WorkShift> Delete(WorkShift workShift);
}
