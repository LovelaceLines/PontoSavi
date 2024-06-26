namespace PontoSavi.Domain.Filters;

public class UserFilter
{
    public string? Search { get; set; }

    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; }

    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = int.MaxValue;

    public bool? IdDescOrderSort { get; set; }
    public bool? NameDescOrderSort { get; set; }
    public bool? UserNameDescOrderSort { get; set; }
    public bool? EmailDescOrderSort { get; set; }

    public int TenantId { get; set; }
}
