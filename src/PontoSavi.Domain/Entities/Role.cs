using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace PontoSavi.Domain.Entities;

public class Role : IdentityRole<int>
{
    override public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int TenantId { get; set; }
    [JsonIgnore]
    public Company? Tenant { get; set; } = null;

    public Role() { }

    public Role(string roleName)
    {
        Name = roleName;
    }

    public Role(Role role)
    {
        Id = role.Id;
        Name = role.Name;
        TenantId = role.TenantId;
        Tenant = role.Tenant;
    }

    [JsonIgnore]
    override public string? NormalizedName { get; set; }
    [JsonIgnore]
    override public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}
