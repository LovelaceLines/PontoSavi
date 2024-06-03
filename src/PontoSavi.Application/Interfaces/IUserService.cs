using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Application.Interfaces;

public interface IUserService
{
    Task<QueryResult<UserDTO>> Query(UserFilter filter);
    Task<UserDTO> QueryById(string id);
    Task<UserDTO> GetByUserName(string userName);
    Task<User> Create(User user, string password);
    Task<User> Update(User user, string userId);
    Task<bool> UpdatePassword(string userId, string oldPassword, string newPassword);
    Task<User> Delete(string id);
}