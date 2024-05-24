using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;

using PontoSavi.API.Utils;
using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.Exceptions;

namespace PontoSavi.API.ServiceFilters;

public class AuthAndUserExtractionFilter : IAsyncActionFilter
{
    private readonly IAuthService _authService;

    public AuthAndUserExtractionFilter(IAuthService authService) =>
        _authService = authService;

    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            throw new AppException("Token não encontrado", HttpStatusCode.Unauthorized);

        var auth = context.HttpContext.Request.Headers.Authorization.ToString();
        auth = AuthUtil.ExtractTokenFromHeader(auth);
        var user = _authService.GetUser(auth).Result;

        context.HttpContext.Items.Add("CurrentUserDTO", user);

        return next();
    }
}