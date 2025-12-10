using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
namespace M03.RFC9457Controller.Controllers;
public class ErrorController: ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var problemDetails = new ProblemDetails
        {
            Type = "https://example.com/probs/internal-server-error",
            Title = "Internal Server Error",
            Status = StatusCodes.Status500InternalServerError,
            Detail = "An unexpected error occurred.",
            Instance = HttpContext.Request.Path
        };
        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
    }

    [Route("/error-development")]
    public IActionResult HandleErrorDevelopment(
    [FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }

        var exception =
            HttpContext.Features.Get<IExceptionHandlerFeature>()!.Error;
        var problemDetails = new ProblemDetails
        {
            Type = "https://example.com/probs/internal-server-error",
            Title = exception?.Message ?? "Unhandled Exception",
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception?.StackTrace,
            Instance = HttpContext.Request.Path
        };
        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };

    }
}