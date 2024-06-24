using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<QueryResult<UserDTO>> Query(UserFilter filter);
    Task<User> Auth(string userName, string password);
    Task<bool> ExistsById(int id, int companyId);
    Task<bool> ExistsByUserName(string userName);
    Task<User> GetById(int id, int companyId);
    Task<User> GetByUserName(string userName);
    Task<List<Role>> GetRoles(User user);
    Task<bool> CheckPassword(User user, string password);
    Task<User> Add(User user, string password);
    new Task<User> Update(User user);
    Task<bool> UpdatePassword(User user, string oldPassword, string newPassword);
}