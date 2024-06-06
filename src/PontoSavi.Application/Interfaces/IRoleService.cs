using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Application.Interfaces;

public interface IRoleService
{
    Task<QueryResult<RoleDTO>> Query(RoleFilter filter);
    Task<Role> GetByPublicId(string publicId);
    Task<List<Role>> GetByUser(User user);
    Task<Role> Create(Role role);
    Task<Role> Update(Role role);
    Task<Role> Delete(string publicId);
}