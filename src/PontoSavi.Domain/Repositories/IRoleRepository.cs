using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<QueryResult<Role>> Query(RoleFilter filter);
    Task<bool> ExistsById(int id, int tenantId);
    Task<bool> ExistsByName(string name, int tenantId);
    Task<Role> GetById(int id, int tenantId);
    Task<Role> GetByName(string name, int tenantId);
    Task<List<Role>> GetByUser(int userId, int tenantId);
    new Task<Role> Update(Role role);
    new Task<Role> Remove(Role role);
}