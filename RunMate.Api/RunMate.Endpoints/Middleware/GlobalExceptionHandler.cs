using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RunMate.Application.Exceptions;

namespace RunMate.Api.Middleware;

/// <summary>
/// Manages exception handling throughout the system.
/// </summary>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    /// <summary>
    /// Handles incoming exceptions and delivers an appropriate response based on the exception type.
    /// </summary>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unhandled exception occurred.");

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An unexpected error occurred."
        };

        if (exception is NotFoundException notFoundException)
        {
            problemDetails.Status = StatusCodes.Status404NotFound;
            problemDetails.Title = notFoundException.Message;
        }

        if (exception is ValidationException validationException)
        {
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Title = validationException.Message;
        }

        if (exception is UnauthorizedException unauthorizedException)
        {
            problemDetails.Status = StatusCodes.Status403Forbidden;
            problemDetails.Title = unauthorizedException.Message;
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}