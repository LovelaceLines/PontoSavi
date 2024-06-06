namespace PontoSavi.Domain.Entities;

public class Company
{
    public int Id { get; set; }
    public string PublicId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string TradeName { get; set; } = null!;
    public string CNPJ { get; set; } = null!;
}
