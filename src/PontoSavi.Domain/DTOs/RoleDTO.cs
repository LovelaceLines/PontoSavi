using Microsoft.AspNetCore.Identity;

namespace PontoSavi.Domain.DTOs;

public class RoleDTO
{
    public string? Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public RoleDTO() { }

    public RoleDTO(IdentityRole role)
    {
        Id = role.Id;
        Name = role.Name!;
    }
}