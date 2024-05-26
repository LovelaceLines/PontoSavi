using Microsoft.AspNetCore.Identity;

using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Application.Interfaces;

public interface IRoleService
{
    Task<QueryResult<RoleDTO>> Query(RoleFilter filter);
    Task<RoleDTO> GetById(string id);
    Task<IdentityRole> Create(IdentityRole role);
    Task<IdentityRole> Update(IdentityRole role);
    Task<bool> Delete(string id);
}