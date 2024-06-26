using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Application.Interfaces;

public interface IUserService
{
    Task<QueryResult<UserDTO>> Query(UserFilter filter);
    Task<User> GetById(int id, int tenantId);
    Task<User> GetByUserName(string userName);
    Task<User> Create(User user, string password);
    Task<User> Update(User user);
    Task<bool> UpdatePassword(User user, string oldPassword, string newPassword);
    Task<User> Delete(User user);
}