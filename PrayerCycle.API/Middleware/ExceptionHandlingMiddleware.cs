using System.Text.Json;
using FluentValidation;
using PrayerCycle.Application.Common.Exceptions;
using PrayerCycle.Domain.Common;

namespace PrayerCycle.API.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Unhandled exception occurred.");

        var (statusCode, title, errors) = exception switch
        {
            ValidationException validationException => (
                StatusCodes.Status400BadRequest,
                "Validation failed.",
                validationException.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(error => error.ErrorMessage).ToArray())),
            NotFoundException notFoundException => (
                StatusCodes.Status404NotFound,
                notFoundException.Message,
                null),
            DomainException domainException => (
                StatusCodes.Status400BadRequest,
                domainException.Message,
                null),
            InvalidOperationException invalidOperationException => (
                StatusCodes.Status409Conflict,
                invalidOperationException.Message,
                null),
            _ => (
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred.",
                null)
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var payload = errors is null
            ? new { title, status = statusCode }
            : (object)new { title, status = statusCode, errors };

        await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
    }
}
