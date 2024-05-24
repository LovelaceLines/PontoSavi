using Microsoft.AspNetCore.Identity;

namespace PontoSavi.Domain.DTOs;

public class UserDTO
{
    public string? Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = [];

    public UserDTO() { }

    public UserDTO(IdentityUser user, List<string>? roles)
    {
        Id = user.Id;
        UserName = user.UserName!;
        Email = user.Email!;
        // EmailConfirmed = user.EmailConfirmed;
        PhoneNumber = user.PhoneNumber!;
        // PhoneNumberConfirmed = user.PhoneNumberConfirmed;
        // TwoFactorEnabled = user.TwoFactorEnabled;
        // LockoutEnd = user.LockoutEnd;
        // LockoutEnabled = user.LockoutEnabled;
        // AccessFailedCount = user.AccessFailedCount;
        Roles = roles ?? [];
    }
}