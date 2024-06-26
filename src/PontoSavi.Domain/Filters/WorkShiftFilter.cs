namespace PontoSavi.Domain.Filters;

public class WorkShiftFilter
{
    public int? Id { get; set; }

    public TimeOnly? CheckIn { get; set; }
    public int? CheckInToleranceMinutes { get; set; }
    public TimeOnly? CheckOut { get; set; }
    public int? CheckOutToleranceMinutes { get; set; }

    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = int.MaxValue;

    public bool? IdDescOrderSort { get; set; }
    public bool? CheckInDescOrderSort { get; set; }
    public bool? CheckOutDescOrderSort { get; set; }

    public int TenantId { get; set; }
}
