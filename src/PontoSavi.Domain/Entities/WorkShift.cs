namespace PontoSavi.Domain.Entities;

public class WorkShift
{
    public int Id { get; set; }
    public TimeOnly CheckIn { get; set; }
    public int CheckInToleranceMinutes { get; set; }
    public TimeOnly CheckOut { get; set; }
    public int CheckOutToleranceMinutes { get; set; }
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int CompanyId { get; set; }
    public Company? Company { get; set; }
}
