using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace PontoSavi.Domain.Entities;

public class Role : IdentityRole<int>
{
    override public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // [JsonIgnore]
    public int CompanyId { get; set; }
    [JsonIgnore]
    public Company? Company { get; set; } = null;

    public Role() { }

    public Role(string roleName)
    {
        Name = roleName;
    }

    public Role(Role role)
    {
        Id = role.Id;
        Name = role.Name;
        CompanyId = role.CompanyId;
        Company = role.Company;
    }

    [JsonIgnore]
    override public string? NormalizedName { get; set; }
    [JsonIgnore]
    override public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}
