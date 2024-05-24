using Microsoft.AspNetCore.Identity;

using PontoSavi.Domain.DTOs;

namespace PontoSavi.Application.Interfaces;

public interface IRoleService
{
    Task<List<RoleDTO>> Query();
    Task<RoleDTO> GetById(int id);
    Task<IdentityRole> Create(IdentityRole role);
    Task<IdentityRole> Update(IdentityRole role);
    Task<bool> Delete(string id);
}