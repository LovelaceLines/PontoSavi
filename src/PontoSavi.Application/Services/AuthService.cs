using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Application.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AuthService(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<AuthToken> Login(string userName, string password)
    {
        var user = await _userRepository.Auth(userName, password);
        return await GetAuthToken(user);
    }

    public async Task<AuthToken> RefreshToken(string refreshToken)
    {
        var result = await new JsonWebTokenHandler().ValidateTokenAsync(refreshToken, new TokenValidationParameters()
        {
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = SecurityKey,
        });

        if (!result.IsValid) throw new AppException("Sessão expirada!", HttpStatusCode.Unauthorized);

        var userPublicId = result.Claims["nameid"].ToString() ?? throw new AppException("Claims NameId not found!", HttpStatusCode.InternalServerError);
        var user = await _userRepository.GetByPublicId(userPublicId);

        return await GetAuthToken(user);
    }

    private async Task<AuthToken> GetAuthToken(User user) =>
        new(accessToken: await GenerateAccessToken(user), expiresIn: ExpiresAccessToken.Minute, refreshToken: GenerateRefreshToken(user));

    private async Task<string> GenerateAccessToken(User user)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = Issuer,
            Audience = Audience,
            Subject = await SubjectAccessToken(user),
            Expires = ExpiresAccessToken,
            SigningCredentials = SigningCredentials,
            TokenType = "at+jwt"
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private string GenerateRefreshToken(User user)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = Issuer,
            Audience = Audience,
            Subject = SubjectRefreshToken(user),
            Expires = ExpiresRefreshToken,
            SigningCredentials = SigningCredentials,
            TokenType = "rt+jwt"
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private SigningCredentials SigningCredentials =>
        new(SecurityKey, SecurityAlgorithms.HmacSha256);

    private SecurityKey SecurityKey =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["Jwt:SecretKey"] ?? throw new AppException("Jwt: Secret is null!", HttpStatusCode.InternalServerError)));

    private string Issuer =>
        _configuration["Jwt:Issuer"] ?? throw new AppException("Jwt: Issuer is null!", HttpStatusCode.InternalServerError);

    private string Audience =>
        _configuration["Jwt:Audience"] ?? throw new AppException("Jwt: Audience is null!", HttpStatusCode.InternalServerError);

    private DateTime ExpiresAccessToken =>
        DateTime.UtcNow.AddHours(int.Parse(
            _configuration["Jwt:HoursAccessTokenExpires"] ?? throw new AppException("Jwt: HoursAccessTokenExpires is null!", HttpStatusCode.InternalServerError)));

    private DateTime ExpiresRefreshToken =>
        DateTime.UtcNow.AddHours(int.Parse(
            _configuration["Jwt:HoursRefreshTokenExpires"] ?? throw new AppException("Jwt: ExpireRefreshTokenHours is null!", HttpStatusCode.InternalServerError)));

    private async Task<ClaimsIdentity> SubjectAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.PublicId.ToString()),
            new(ClaimTypes.Name, user.UserName!)
        };

        var roles = await _userRepository.GetRoles(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return new ClaimsIdentity(claims);
    }

    private ClaimsIdentity SubjectRefreshToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.PublicId.ToString()),
        };

        return new ClaimsIdentity(claims);
    }

    public async Task<UserDTO> GetUser(string token)
    {
        var result = await new JsonWebTokenHandler().ValidateTokenAsync(token, new TokenValidationParameters()
        {
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = SecurityKey,
        });
        if (!result.IsValid) throw new AppException("Sessão expirada!", HttpStatusCode.Unauthorized);

        var userPublicId = result.Claims["nameid"].ToString() ?? throw new AppException("Claims NameId not found!", HttpStatusCode.InternalServerError);
        var user = await _userRepository.GetByPublicId(userPublicId);
        var roles = await _userRepository.GetRoles(user);

        return new UserDTO(user, roles);
    }

    public async Task<string> GetUserPublicId(string token)
    {
        var result = await new JsonWebTokenHandler().ValidateTokenAsync(token, new TokenValidationParameters()
        {
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = SecurityKey,
        });
        if (!result.IsValid) throw new AppException("Sessão expirada!", HttpStatusCode.Unauthorized);

        return result.Claims["nameid"].ToString() ?? throw new AppException("Claims NameId not found!", HttpStatusCode.InternalServerError);
    }

    public async Task<string[]> GetUserRoles(string token)
    {
        var result = await new JsonWebTokenHandler().ValidateTokenAsync(token, new TokenValidationParameters()
        {
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = SecurityKey,
        });
        if (!result.IsValid) throw new AppException("Sessão expirada!", HttpStatusCode.Unauthorized);

        var rolesSTR = result.Claims["role"].ToString() ?? throw new AppException("Claims Role not found!", HttpStatusCode.InternalServerError);
        var roles = rolesSTR.Split(',');
        return roles;
    }
}