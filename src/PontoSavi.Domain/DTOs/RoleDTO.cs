using PontoSavi.Domain.Entities;

namespace PontoSavi.Domain.DTOs;

public class RoleDTO
{
    public string? PublicId { get; set; } = null!;
    public string Name { get; set; } = null!;

    public RoleDTO() { }

    public RoleDTO(Role role)
    {
        PublicId = role.PublicId;
        Name = role.Name!;
    }
}