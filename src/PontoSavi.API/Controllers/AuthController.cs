using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PontoSavi.API.InputModels;
using PontoSavi.Application.Interfaces;
using PontoSavi.API.Utils;
using PontoSavi.Domain.DTOs;

namespace PontoSavi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public AuthController(IAuthService authService,
        IUserService userService,
        IRoleService roleService)
    {
        _authService = authService;
        _userService = userService;
        _roleService = roleService;
    }

    /// <summary>
    /// Authenticates a user and returns an auth token.
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<UserToken>> Login([FromBody] LoginIM login)
    {
        var token = await _authService.Login(login.UserName, login.Password);
        var user = await _userService.GetByUserName(login.UserName);
        var roles = await _roleService.GetByUser(user);

        return Ok(new UserToken(token, new UserDTO(user, roles)));
    }

    /// <summary>
    /// Refreshes an auth token.
    /// </summary>
    [HttpGet("refresh-token")]
    public async Task<ActionResult<AuthToken>> RefreshToken([FromHeader(Name = "Authorization")] string auth)
    {
        var token = AuthUtil.ExtractTokenFromHeader(auth);
        return Ok(await _authService.RefreshToken(token));
    }

    /// <summary>
    /// Retrieves the user information based on the provided authorization token. Note: Authorization token in the format "Bearer {token}".
    /// </summary>
    [Authorize]
    [HttpGet("user")]
    public async Task<ActionResult<UserDTO>> Get([FromHeader(Name = "Authorization")] string auth)
    {
        var token = AuthUtil.ExtractTokenFromHeader(auth);
        return Ok(await _authService.GetUser(token));
    }
}