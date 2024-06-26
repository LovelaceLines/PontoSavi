namespace PontoSavi.Domain.Entities;

public class CompanyWorkShift : Base
{
    public int WorkShiftId { get; set; }
    public WorkShift? WorkShift { get; set; }
}
