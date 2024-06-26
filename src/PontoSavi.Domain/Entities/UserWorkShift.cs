namespace PontoSavi.Domain.Entities;

public class UserWorkShift : Base
{
    public int UserId { get; set; }
    public User? User { get; set; }
    public int WorkShiftId { get; set; }
    public WorkShift? WorkShift { get; set; }
}
