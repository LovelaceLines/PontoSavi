using Microsoft.AspNetCore.Identity;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Domain.DTOs;

public class RoleDTO
{
    public string? Id { get; set; } = null!;
    public string Name { get; set; } = null!;

    public RoleDTO() { }

    public RoleDTO(Role role)
    {
        Id = role.Id;
        Name = role.Name!;
    }
}