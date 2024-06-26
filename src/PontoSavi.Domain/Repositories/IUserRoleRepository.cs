using PontoSavi.Domain.Entities;

namespace PontoSavi.Domain.Repositories;

public interface IUserRoleRepository : IBaseRepository<UserRole>
{
    Task<bool> Exists(int userId, int roleId, int tenantId);
    Task<UserRole> Get(int userId, int roleId, int tenantId);
    Task<UserRole> Add(int userId, int roleId, int tenantId);
}