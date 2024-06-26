namespace PontoSavi.Domain.Entities;

public class Base
{
    public int TenantId { get; set; }
    public Company? Tenant { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
