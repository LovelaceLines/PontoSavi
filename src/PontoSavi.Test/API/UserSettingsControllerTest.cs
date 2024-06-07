using PontoSavi.API.ViewModels;
using PontoSavi.Domain.DTOs;
using PontoSavi.Test.Fakers;
using PontoSavi.Test.Global;

namespace PontoSavi.Test.API;

public class UserSettingsControllerTest : GlobalClientRequest
{
    [Fact]
    public async Task Post_ValidUserSettings_ReturnsOkResult()
    {
        var user = await GetUser();
        var userSettings = new UserSettingsFake(userPublicId: user.PublicId).Generate();

        var result = await PostFromBody<UserDTO>(_userSettingsClient, userSettings);

        Assert.Equal(user.PublicId, result.PublicId);
        Assert.Equal(userSettings.CheckIn, result.userSettings!.CheckIn);
        Assert.Equal(userSettings.CheckInToleranceMinutes, result.userSettings!.CheckInToleranceMinutes);
        Assert.Equal(userSettings.CheckOut, result.userSettings!.CheckOut);
        Assert.Equal(userSettings.CheckOutToleranceMinutes, result.userSettings!.CheckOutToleranceMinutes);
    }

    [Fact]
    public async Task Put_ValidUserSettings_ReturnsOkResult()
    {
        var userSettings = await GetUserSettings();
        var userSettingsFake = new UserSettingsFake(userPublicId: userSettings.PublicId).Generate();

        var result = await PutFromBody<UserDTO>(_userSettingsClient, userSettingsFake);

        Assert.Equal(userSettings.PublicId, result.PublicId);
        Assert.Equal(userSettingsFake.CheckIn, result.userSettings!.CheckIn);
        Assert.Equal(userSettingsFake.CheckInToleranceMinutes, result.userSettings!.CheckInToleranceMinutes);
        Assert.Equal(userSettingsFake.CheckOut, result.userSettings!.CheckOut);
        Assert.Equal(userSettingsFake.CheckOutToleranceMinutes, result.userSettings!.CheckOutToleranceMinutes);
    }
}
