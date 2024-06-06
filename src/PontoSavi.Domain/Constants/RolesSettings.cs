using Microsoft.Extensions.Configuration;
using System.Net;

using PontoSavi.Domain.Exceptions;

namespace PontoSavi.Domain.Constants;

public class RolesSettings
{
    public List<string> AllStandardUserRoles { get; set; }
    public List<string> BaseUserRoles { get; set; }
    public List<string> SuperUserRoles { get; set; }
    public List<string> AdminUserRoles { get; set; }

    public RolesSettings(IConfiguration configuration)
    {
        AllStandardUserRoles = configuration.GetSection("GlobalSettings:AllStandardUserRoles").Value?.Split(',').ToList() ??
            throw new AppException("GlobalSettings:AllStandardUserRoles is null!", HttpStatusCode.InternalServerError);

        BaseUserRoles = configuration.GetSection("GlobalSettings:BaseUserRoles").Value?.Split(',').ToList() ??
            throw new AppException("GlobalSettings:BaseUserRoles is null!", HttpStatusCode.InternalServerError);

        SuperUserRoles = configuration.GetSection("GlobalSettings:SuperUserRoles").Value?.Split(',').ToList() ??
            throw new AppException("GlobalSettings:SuperUserRoles is null!", HttpStatusCode.InternalServerError);

        AdminUserRoles = configuration.GetSection("GlobalSettings:AdminUserRoles").Value?.Split(',').ToList() ??
            throw new AppException("GlobalSettings:AdminUserRoles is null!", HttpStatusCode.InternalServerError);
    }
}

public interface IRolesSettingsService
{
    bool IsStandardUser(string role);
    bool IsBaseUser(string role);
    bool IsSuperUser(string role);
    bool IsAdminUser(string role);
    string[] GetStandardUserRoles();
    string[] GetBaseUserRoles();
    string[] GetSuperUserRoles();
    string[] GetAdminUserRoles();
}

public class RolesSettingsService : IRolesSettingsService
{
    private readonly RolesSettings _globalSettings;

    public RolesSettingsService(IConfiguration configuration) =>
        _globalSettings = new RolesSettings(configuration);

    public bool IsStandardUser(string role) =>
        _globalSettings.AllStandardUserRoles.Contains(role);

    public bool IsBaseUser(string role) =>
        _globalSettings.BaseUserRoles.Contains(role);

    public bool IsSuperUser(string role) =>
        _globalSettings.SuperUserRoles.Contains(role);

    public bool IsAdminUser(string role) =>
        _globalSettings.AdminUserRoles.Contains(role);

    public string[] GetStandardUserRoles() =>
        _globalSettings.AllStandardUserRoles.ToArray();

    public string[] GetBaseUserRoles() =>
        _globalSettings.BaseUserRoles.ToArray();

    public string[] GetSuperUserRoles() =>
        _globalSettings.SuperUserRoles.ToArray();

    public string[] GetAdminUserRoles() =>
        _globalSettings.AdminUserRoles.ToArray();
}
