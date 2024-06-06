using PontoSavi.Domain.Entities;

namespace PontoSavi.Domain.DTOs;

public class UserDTO
{
    public string? PublicId { get; set; } = string.Empty;
    public string UserName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = [];

    public UserDTO() { }

    public UserDTO(User user)
    {
        PublicId = user.PublicId!;
        UserName = user.UserName!;
        Name = user.Name!;
        Email = user.Email!;
        PhoneNumber = user.PhoneNumber!;
    }

    public UserDTO(User user, List<string> roles)
    {
        PublicId = user.PublicId!;
        UserName = user.UserName!;
        Name = user.Name!;
        Email = user.Email!;
        PhoneNumber = user.PhoneNumber!;
        Roles = roles;
    }

    public UserDTO(User user, List<Role> roles)
    {
        PublicId = user.PublicId!;
        UserName = user.UserName!;
        Name = user.Name!;
        Email = user.Email!;
        PhoneNumber = user.PhoneNumber!;
        Roles = roles.Select(r => r.Name!).ToList();
    }
}