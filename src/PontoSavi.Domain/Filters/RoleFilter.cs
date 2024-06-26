namespace PontoSavi.Domain.Filters;

public class RoleFilter
{
    public string? Search { get; set; }

    public int? Id { get; set; }
    public string? Name { get; set; }

    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = int.MaxValue;

    public bool? NameDescOrderSort { get; set; }

    public int TenantId { get; set; }
}
