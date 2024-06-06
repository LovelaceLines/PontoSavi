using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Application.Interfaces;

public interface IUserService
{
    Task<QueryResult<UserDTO>> Query(UserFilter filter);
    Task<User> GetByPublicId(string publicId);
    Task<User> GetByUserName(string userName);
    Task<User> Create(User user, string password);
    Task<User> Update(User user);
    Task<bool> UpdatePassword(int id, string oldPassword, string newPassword);
    Task<User> Delete(string id);
}