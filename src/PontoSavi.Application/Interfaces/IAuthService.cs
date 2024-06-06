using PontoSavi.Domain.DTOs;

namespace PontoSavi.Application.Interfaces;

public interface IAuthService
{
    Task<AuthToken> Login(string userName, string password);
    Task<AuthToken> RefreshToken(string refreshToken);
    Task<UserDTO> GetUser(string acessToken);
    Task<string> GetUserPublicId(string acessToken);
    Task<string[]> GetUserRoles(string acessToken);
}