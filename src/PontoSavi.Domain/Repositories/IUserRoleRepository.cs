using Microsoft.AspNetCore.Identity;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Domain.Repositories;

public interface IUserRoleRepository : IBaseRepository<IdentityUserRole<string>>
{
    Task<bool> ExistsByUserAndRoleName(User user, string roleName);
    Task<bool> ExistsByUserIdAndRoleName(string userId, string roleName);
    Task<List<string>> GetRolesByUserId(int userId);
    Task<bool> Add(User user, string roleName);
    Task<bool> Remove(User user, string roleName);
}