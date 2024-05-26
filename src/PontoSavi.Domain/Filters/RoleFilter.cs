namespace PontoSavi.Domain.Filters;

public class RoleFilter
{
    public string? Search { get; set; }

    public string? Id { get; set; }
    public string? Name { get; set; }

    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = int.MaxValue;

    public string? NameOrderSort { get; set; }
}
