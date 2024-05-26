using Microsoft.AspNetCore.Identity;

using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Application.Interfaces;

public interface IUserService
{
    Task<QueryResult<UserDTO>> Query(UserFilter filter);
    Task<UserDTO> QueryById(string id);
    Task<UserDTO> GetByUserName(string userName);
    Task<IdentityUser> Create(IdentityUser user, string password);
    Task<IdentityUser> Update(IdentityUser user, string userId);
    Task<bool> UpdatePassword(string userId, string oldPassword, string newPassword);
    Task<IdentityUser> Delete(string id);
}