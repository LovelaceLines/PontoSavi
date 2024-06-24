using PontoSavi.Domain.Entities;

namespace PontoSavi.Domain.DTOs;

public class WorkShiftDTO : WorkShift
{
    public UserDTO? User { get; set; }

    public WorkShiftDTO(WorkShift workShift)
    {
        Id = workShift.Id;
        CheckIn = workShift.CheckIn;
        CheckInToleranceMinutes = workShift.CheckInToleranceMinutes;
        CheckOut = workShift.CheckOut;
        CheckOutToleranceMinutes = workShift.CheckOutToleranceMinutes;
        Description = workShift.Description;
        CreatedAt = workShift.CreatedAt;
        UpdatedAt = workShift.UpdatedAt;
        CompanyId = workShift.CompanyId;
    }

    public WorkShiftDTO(WorkShift workShift, User? user, Company? company) : this(workShift)
    {
        User = user is null ? null : new UserDTO(user);
        Company = company;
    }
}