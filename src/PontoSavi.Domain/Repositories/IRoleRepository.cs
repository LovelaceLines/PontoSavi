using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<QueryResult<Role>> Query(RoleFilter filter);
    Task<bool> ExistsById(int id, int companyId);
    Task<bool> ExistsByName(string name, int companyId);
    Task<Role> GetById(int id, int companyId);
    Task<Role> GetByName(string name, int companyId);
    Task<List<Role>> GetByUser(int userId, int companyId);
    // new Task<Role> Add(Role role);
    new Task<Role> Update(Role role);
    new Task<Role> Remove(Role role);
}