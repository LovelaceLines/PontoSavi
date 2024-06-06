namespace PontoSavi.Domain.Filters;

public class CompanyFilter
{
    public string? Search { get; set; }

    public string? PublicId { get; set; } = null!;
    public string? Name { get; set; } = null!;
    public string? TradeName { get; set; } = null!;
    public string? CNPJ { get; set; } = null!;

    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = int.MaxValue;

    public bool? NameDescOrderSort { get; set; }
    public bool? TradeNameDescOrderSort { get; set; }
    public bool? CNPJDescOrderSort { get; set; }
}
