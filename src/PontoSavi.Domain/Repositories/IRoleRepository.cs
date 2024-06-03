using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<QueryResult<RoleDTO>> Query(RoleFilter filter);
    Task<bool> ExistsById(string id);
    Task<bool> ExistsByName(string name);
    Task<Role> GetById(string id);
    Task<Role> GetByName(string name);
    new Task<Role> Add(Role role);
    new Task<Role> Update(Role role);
    Task<Role> Remove(string name);
}