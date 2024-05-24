using Microsoft.AspNetCore.Identity;
using System.Net;

using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Context;

namespace PontoSavi.Infra.Data.Repositories;

public class UserRoleRepository : BaseRepository<IdentityUserRole<string>>, IUserRoleRepository
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public UserRoleRepository(AppDbContext context, UserManager<IdentityUser> userManager) : base(context)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<bool> Add(IdentityUser user, string roleName)
    {
        var result = await _userManager.AddToRoleAsync(user, roleName);
        return result.Succeeded ? true : throw new AppException("Erro ao adicionar usuário ao perfil!", HttpStatusCode.BadRequest);
    }

    public async Task<bool> ExistsByUserAndRoleName(IdentityUser user, string roleName) =>
        await _userManager.IsInRoleAsync(user, roleName);

    public async Task<bool> ExistsByUserIdAndRoleName(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId) ?? throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);

        return await _userManager.IsInRoleAsync(user, roleName);
    }

    public async Task<List<string>> GetRolesByUserId(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId) ?? throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);
        return (List<string>)await _userManager.GetRolesAsync(user);
    }

    public async Task<bool> Remove(IdentityUser user, string roleName)
    {
        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
        return result.Succeeded ? true : throw new AppException("Erro ao remover usuário do perfil!", HttpStatusCode.BadRequest);
    }
}