using Microsoft.AspNetCore.Identity;

using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<IdentityRole>
{
    Task<QueryResult<RoleDTO>> Query(RoleFilter filter);
    Task<bool> ExistsById(string id);
    Task<bool> ExistsByName(string name);
    Task<IdentityRole> GetById(string id);
    Task<IdentityRole> GetByName(string name);
    new Task<IdentityRole> Add(IdentityRole role);
    new Task<IdentityRole> Update(IdentityRole role);
    Task<IdentityRole> Remove(string name);
}