using Microsoft.AspNetCore.Identity;

namespace PontoSavi.Domain.Entities;

public class UserRole : IdentityUserRole<int>
{
    override public int UserId { get; set; }
    public User? User { get; set; }
    override public int RoleId { get; set; }
    public Role? Role { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int TenantId { get; set; }
    public Company? Tenant { get; set; }
}
