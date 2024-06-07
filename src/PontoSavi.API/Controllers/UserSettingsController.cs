using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.API.ViewModels;

namespace PontoSavi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
// [ServiceFilter(typeof(AuthAndUserExtractionFilter))]
public class UserSettingsController : ControllerBase
{
    private readonly IUserSettingsService _userSettingsService;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public UserSettingsController(IUserSettingsService userSettingsService,
        IUserService userService,
        IRoleService roleService,
        IMapper mapper)
    {
        _userSettingsService = userSettingsService;
        _userService = userService;
        _roleService = roleService;
        _mapper = mapper;
    }

    [HttpGet("{userId}")]
    [Authorize]
    public async Task<ActionResult<UserSettingsVM>> GetByUserId(int userId)
    {
        var userSettings = await _userSettingsService.GetByUserId(userId);
        return Ok(_mapper.Map<UserSettingsVM>(userSettings));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<UserDTO>> Create([FromBody] UserSettingsVM userSettingsVM)
    {
        var user = await _userService.GetByPublicId(userSettingsVM.UserPublicId);

        var userSettings = _mapper.Map<UserSettings>(userSettingsVM);
        userSettings.UserId = user.Id;
        userSettings.User = null;

        userSettings = await _userSettingsService.Create(userSettings);

        var roles = await _roleService.GetByUser(user);
        return Ok(new UserDTO(user, roles, userSettings));
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<UserDTO>> Update([FromBody] UserSettingsVM userSettingsVM)
    {
        var user = await _userService.GetByPublicId(userSettingsVM.UserPublicId);

        var userSettings = _mapper.Map<UserSettings>(userSettingsVM);
        userSettings.UserId = user.Id;
        userSettings.User = null;

        userSettings = await _userSettingsService.Update(userSettings);

        var roles = await _roleService.GetByUser(user);
        return Ok(new UserDTO(user, roles, userSettings));
    }
}
