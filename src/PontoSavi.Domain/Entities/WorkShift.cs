namespace PontoSavi.Domain.Entities;

public class WorkShift : Base
{
    public int Id { get; set; }
    public TimeOnly CheckIn { get; set; }
    public int CheckInToleranceMinutes { get; set; }
    public TimeOnly CheckOut { get; set; }
    public int CheckOutToleranceMinutes { get; set; }
    public string? Description { get; set; }
}
