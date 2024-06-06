using Microsoft.AspNetCore.Identity;
using System.Net;

using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Context;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.Data.Repositories;

public class UserRoleRepository : BaseRepository<IdentityUserRole<string>>, IUserRoleRepository
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;

    public UserRoleRepository(AppDbContext context, UserManager<User> userManager) : base(context)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<bool> Add(User user, string roleName)
    {
        var result = await _userManager.AddToRoleAsync(user, roleName);
        return result.Succeeded ? true : throw new AppException("Erro ao adicionar usuário ao perfil!", HttpStatusCode.BadRequest);
    }

    public async Task<bool> ExistsByUserAndRoleName(User user, string roleName) =>
        await _userManager.IsInRoleAsync(user, roleName);

    public async Task<bool> ExistsByUserIdAndRoleName(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId) ?? throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);

        return await _userManager.IsInRoleAsync(user, roleName);
    }

    public async Task<List<string>> GetRolesByUserId(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);
        return (List<string>)await _userManager.GetRolesAsync(user);
    }

    public async Task<bool> Remove(User user, string roleName)
    {
        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
        return result.Succeeded ? true : throw new AppException("Erro ao remover usuário do perfil!", HttpStatusCode.BadRequest);
    }
}