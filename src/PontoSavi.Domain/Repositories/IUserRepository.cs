using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<QueryResult<UserDTO>> Query(UserFilter filter);
    Task<UserDTO> QueryById(string id);
    Task<User> Auth(string userName, string password);
    Task<bool> ExistsById(string id);
    Task<bool> ExistsByUserName(string userName);
    Task<bool> ExistsByEmail(string email);
    Task<bool> ExistsByPhoneNumber(string phoneNumber);
    Task<User> GetById(string id);
    Task<User> GetByUserName(string userName);
    Task<User> GetByEmail(string email);
    Task<User> GetByPhoneNumber(string phoneNumber);
    Task<List<string>> GetRoles(User user);
    Task<bool> CheckPassword(string id, string password);
    Task<bool> CheckPassword(User user, string password);
    Task<User> Add(User user, string password);
    new Task<User> Update(User user);
    Task<bool> UpdatePassword(string id, string oldPassword, string newPassword);
    Task<bool> RemoveByUserName(string userName);
}