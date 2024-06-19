using PontoSavi.Domain.Entities;

namespace PontoSavi.Application.Interfaces;

public interface IUserRoleService
{
    Task<bool> AddToRole(User user, Role role);
    Task<bool> RemoveFromRole(User user, Role role);
}