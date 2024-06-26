using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Application.Interfaces;

public interface IRoleService
{
    Task<QueryResult<Role>> Query(RoleFilter filter);
    Task<Role> GetById(int id, int tenantId);
    Task<Role> GetByName(string name, int tenantId);
    Task<List<Role>> GetByUser(int id, int tenantId);
    Task<Role> Create(Role role);
    Task<Role> Update(Role role);
    Task<Role> Delete(Role role);
}