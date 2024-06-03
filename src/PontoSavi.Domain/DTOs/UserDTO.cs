using Microsoft.AspNetCore.Identity;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Domain.DTOs;

public class UserDTO
{
    public string? Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public List<string> Roles { get; set; } = [];

    public UserDTO() { }

    public UserDTO(User user)
    {
        Id = user.Id;
        UserName = user.UserName!;
        Name = user.Name!;
        Email = user.Email!;
        PhoneNumber = user.PhoneNumber!;
    }

    public UserDTO(User user, List<string>? roles)
    {
        Id = user.Id;
        UserName = user.UserName!;
        Name = user.Name!;
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