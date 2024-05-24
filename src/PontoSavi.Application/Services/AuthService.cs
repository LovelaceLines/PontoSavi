using Microsoft.AspNetCore.Identity;
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

        var userId = result.Claims["nameid"].ToString() ?? throw new AppException("Claims NameId not found!", HttpStatusCode.InternalServerError);
        var user = await _userRepository.GetById(userId);

        return await GetAuthToken(user);
    }

    private async Task<AuthToken> GetAuthToken(IdentityUser user) =>
        new(accessToken: await GenerateAccessToken(user), expiresIn: ExpiresAccessToken.Minute, refreshToken: GenerateRefreshToken(user));

    private async Task<string> GenerateAccessToken(IdentityUser user)
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

    private string GenerateRefreshToken(IdentityUser user)
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
            _configuration["SecretKey"] ?? throw new AppException("JwtConfig: Secret is null!", HttpStatusCode.InternalServerError)));

    private string Issuer =>
        _configuration["Issuer"] ?? throw new AppException("JwtConfig: Issuer is null!", HttpStatusCode.InternalServerError);

    private string Audience =>
        _configuration["Audience"] ?? throw new AppException("JwtConfig: Audience is null!", HttpStatusCode.InternalServerError);

    private DateTime ExpiresAccessToken =>
        DateTime.UtcNow.AddHours(int.Parse(
            _configuration["HoursAccessTokenExpires"] ?? throw new AppException("JwtConfig: HoursAccessTokenExpires is null!", HttpStatusCode.InternalServerError)));

    private DateTime ExpiresRefreshToken =>
        DateTime.UtcNow.AddHours(int.Parse(
            _configuration["HoursRefreshTokenExpires"] ?? throw new AppException("JwtConfig: ExpireRefreshTokenHours is null!", HttpStatusCode.InternalServerError)));

    private async Task<ClaimsIdentity> SubjectAccessToken(IdentityUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!)
        };

        var roles = await _userRepository.GetRoles(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return new ClaimsIdentity(claims);
    }

    private ClaimsIdentity SubjectRefreshToken(IdentityUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
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

        var userId = result.Claims["nameid"].ToString() ?? throw new AppException("Claims NameId not found!", HttpStatusCode.InternalServerError);
        var user = await _userRepository.GetById(userId);
        var roles = await _userRepository.GetRoles(user);

        return new UserDTO(user, roles);
    }
}