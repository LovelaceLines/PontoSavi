namespace PontoSavi.Domain.Filters;

public class UserFilter
{
    public string? Search { get; set; }

    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; }

    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = int.MaxValue;

    public string? UserNameOrderSort { get; set; }
    public string? EmailOrderSort { get; set; }
}
