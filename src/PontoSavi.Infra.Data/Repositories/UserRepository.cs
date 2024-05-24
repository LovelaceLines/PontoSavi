using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Context;

namespace PontoSavi.Infra.Data.Repositories;

public class UserRepository : BaseRepository<IdentityUser>, IUserRepository
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public UserRepository(AppDbContext context, UserManager<IdentityUser> userManager) : base(context)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IdentityUser> Auth(string userName, string password)
    {
        if (!await ExistsByUserName(userName)) throw new AppException("Erro ao fazer login!", HttpStatusCode.Unauthorized);
        var user = await GetByUserName(userName);

        if (!await CheckPassword(user, password)) throw new AppException("Erro ao fazer login!", HttpStatusCode.Unauthorized);

        return user;
    }

    public async Task<bool> CheckPassword(string id, string password) =>
        await _userManager.CheckPasswordAsync(await GetById(id), password);

    public async Task<bool> CheckPassword(IdentityUser user, string password) =>
        await _userManager.CheckPasswordAsync(user, password);

    public async Task<bool> ExistsByEmail(string email) =>
        await _userManager.Users.AsNoTracking().AnyAsync(u => u.Email == email);

    public async Task<bool> ExistsById(string id) =>
        await _userManager.Users.AsNoTracking().AnyAsync(u => u.Id == id);

    public async Task<bool> ExistsByPhoneNumber(string phoneNumber) =>
        await _userManager.Users.AsNoTracking().AnyAsync(u => u.PhoneNumber == phoneNumber);

    public async Task<bool> ExistsByUserName(string userName) =>
        await _userManager.Users.AsNoTracking().AnyAsync(u => u.UserName == userName);

    public async Task<IdentityUser> GetByEmail(string email) =>
        await _userManager.FindByEmailAsync(email) ?? throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);

    public async Task<IdentityUser> GetById(string id) =>
        await _userManager.FindByIdAsync(id) ?? throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);

    public async Task<IdentityUser> GetByPhoneNumber(string phoneNumber) =>
        await _userManager.Users.AsNoTracking().FirstAsync(u => u.PhoneNumber == phoneNumber);

    public async Task<IdentityUser> GetByUserName(string userName) =>
        await _userManager.FindByNameAsync(userName) ?? throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);

    public async Task<List<string>> GetRoles(IdentityUser user) =>
        (List<string>)await _userManager.GetRolesAsync(user);

    public async Task<IdentityUser> Add(IdentityUser user, string password)
    {
        user.Id = Guid.NewGuid().ToString();
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded) CatchError(new Exception(result.Errors.First().Description));

        return user;
    }

    public new async Task<IdentityUser> Update(IdentityUser user)
    {
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded) CatchError(new Exception(result.Errors.First().Description));

        return user;
    }

    public async Task<bool> UpdatePassword(string id, string oldPassword, string newPassword)
    {
        var user = await GetById(id);
        var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        if (!result.Succeeded) throw new AppException(result.Errors.First().Description, HttpStatusCode.BadRequest);

        return true;
    }

    public async Task<bool> RemoveByUserName(string userName)
    {
        var user = await GetByUserName(userName);

        await _userManager.DeleteAsync(user);

        return true;
    }
}