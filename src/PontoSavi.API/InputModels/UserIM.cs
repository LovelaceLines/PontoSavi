using PontoSavi.Domain.Entities;

namespace PontoSavi.API.InputModels;

public class UserAndPassword : User
{
    public string Password { get; set; } = null!;
}

public class UserRoleIM
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public int TenantId { get; set; }
}

public class UpdatePasswordIM
{
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}