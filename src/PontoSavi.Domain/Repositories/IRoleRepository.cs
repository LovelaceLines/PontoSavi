using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<QueryResult<RoleDTO>> Query(RoleFilter filter);
    Task<bool> ExistsById(int id);
    Task<bool> ExistsByPublicId(string publicId);
    Task<bool> ExistsByName(string name);
    Task<Role> GetById(int id);
    Task<Role> GetByPublicId(string publicId);
    Task<Role> GetByName(string name);
    Task<List<Role>> GetByUser(User user);
    new Task<Role> Add(Role role);
    new Task<Role> Update(Role role);
    Task<Role> Remove(string name);
}