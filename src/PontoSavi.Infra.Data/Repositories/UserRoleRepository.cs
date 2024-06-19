using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Net;

using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Context;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.Data.Repositories;

public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
{
    private readonly AppDbContext _context;

    public UserRoleRepository(AppDbContext context, UserManager<User> userManager) : base(context) =>
        _context = context;

    public async Task<bool> Exists(int userId, int roleId, int companyId) =>
        await _context.UserRoles.AsNoTracking().AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId && ur.CompanyId == companyId);

    public async Task<UserRole> Get(int userId, int roleId, int companyId) =>
        await _context.UserRoles.AsNoTracking().FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId && ur.CompanyId == companyId) ??
            throw new AppException("Relação não encontrada!", HttpStatusCode.NotFound);

    public async Task<UserRole> Add(int userId, int roleId, int companyId)
    {
        UserRole userRole = new()
        {
            UserId = userId,
            RoleId = roleId,
            CompanyId = companyId
        };

        return await Add(userRole);
    }
}