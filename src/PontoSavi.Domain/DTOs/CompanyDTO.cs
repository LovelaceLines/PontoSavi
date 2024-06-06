using PontoSavi.Domain.Entities;

namespace PontoSavi.Domain.DTOs;

public class CompanyDTO
{
    public int? Id { get; set; }
    public string? PublicId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string TradeName { get; set; } = null!;
    public string CNPJ { get; set; } = null!;

    public CompanyDTO() { }

    public CompanyDTO(Company company)
    {
        PublicId = company.PublicId;
        Name = company.Name;
        TradeName = company.TradeName;
        CNPJ = company.CNPJ;
    }
}
