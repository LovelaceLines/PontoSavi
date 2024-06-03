using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Application.Interfaces;

public interface IRoleService
{
    Task<QueryResult<RoleDTO>> Query(RoleFilter filter);
    Task<RoleDTO> GetById(string id);
    Task<Role> Create(Role role);
    Task<Role> Update(Role role);
    Task<bool> Delete(string id);
}