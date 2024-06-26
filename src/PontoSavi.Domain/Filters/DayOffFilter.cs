namespace PontoSavi.Domain.Filters;

public class DayOffFilter
{
    public string? Search { get; set; }

    public int? Id { get; set; }
    public DateTime? Date { get; set; }
    public string? Description { get; set; }

    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = int.MaxValue;

    public bool? IdDescOrderSort { get; set; }
    public bool? DateDescOrderSort { get; set; }

    public int TenantId { get; set; }
}
