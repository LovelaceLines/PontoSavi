using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace PontoSavi.Domain.Entities;

public class User : IdentityUser<int>
{
    public string Name { get; set; } = null!;
    override public string UserName { get; set; } = null!;
    override public string Email { get; set; } = null!;
    override public string PhoneNumber { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int TenantId { get; set; }
    public Company? Tenant { get; set; }

    public User() { }

    public User(string userName)
    {
        UserName = userName;
    }

    [JsonIgnore]
    override public string? NormalizedUserName { get; set; }
    [JsonIgnore]
    override public string? NormalizedEmail { get; set; }
    [JsonIgnore]
    override public bool EmailConfirmed { get; set; }
    [JsonIgnore]
    override public string? PasswordHash { get; set; }
    [JsonIgnore]
    override public string? SecurityStamp { get; set; }
    [JsonIgnore]
    override public string? ConcurrencyStamp { get; set; }
    [JsonIgnore]
    override public bool PhoneNumberConfirmed { get; set; }
    [JsonIgnore]
    override public bool TwoFactorEnabled { get; set; }
    [JsonIgnore]
    override public DateTimeOffset? LockoutEnd { get; set; }
    [JsonIgnore]
    override public bool LockoutEnabled { get; set; }
    [JsonIgnore]
    override public int AccessFailedCount { get; set; }
}
