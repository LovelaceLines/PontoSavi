using PontoSavi.Domain.Entities;

namespace PontoSavi.Domain.Filters;

public class PointFilter
{
    public string? Search { get; set; }

    public int? Id { get; set; }
    public int? UserId { get; set; }
    public int? ManagerId { get; set; }
    public DateTime? CheckIn { get; set; }
    public PointStatus? CheckInStatus { get; set; }
    public DateTime? CheckOut { get; set; }
    public PointStatus? CheckOutStatus { get; set; }

    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = int.MaxValue;

    public bool? IdDescOrderSort { get; set; }
    public bool? CheckInDescOrderSort { get; set; }
    public bool? CheckOutDescOrderSort { get; set; }

    public int TenantId { get; set; }
}
