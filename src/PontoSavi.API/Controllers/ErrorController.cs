using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

using PontoSavi.Domain.Exceptions;

namespace PontoSavi.API.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
[Route("api/[controller]")]
public class ErrorController : ControllerBase
{
    public ActionResult<AppException> Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var serverErrorMessage = "Um erro inesperado acaba de acontecer! Por favor, contate a equipe de desenvolvimento para a análise e correção do erro.";
        var message = (context?.Error as AppException)?.Message ?? serverErrorMessage;
        var statusCode = (context?.Error as AppException)?.StatusCode ?? HttpStatusCode.InternalServerError;
        var id = HttpContext.TraceIdentifier;

        return StatusCode((int)statusCode, new AppException(id, message, statusCode));
    }
}