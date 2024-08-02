using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Exceptions;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Exception occured {Message}", exception.Message);

        var propblemDetails = new ProblemDetails()
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Error",
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = propblemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(propblemDetails, cancellationToken);

        return true;
    }
}