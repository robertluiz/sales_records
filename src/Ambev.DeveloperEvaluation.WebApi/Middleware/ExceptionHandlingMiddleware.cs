using System.Net;
using System.Text.Json;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware;

/// <summary>
/// Middleware for handling exceptions globally across the application
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    /// <summary>
    /// Initializes a new instance of the middleware
    /// </summary>
    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    /// <summary>
    /// Invokes the middleware
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ApiResponse
        {
            Success = false,
            Message = "An error occurred while processing your request"
        };

        switch (exception)
        {
            case ValidationException validationEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Validation error";
                response.Errors = validationEx.Errors.Select(error => (ValidationErrorDetail)error);
                break;

            case InvalidOperationException operationEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = operationEx.Message;
                break;

            case UnauthorizedAccessException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response.Message = "Unauthorized access";
                break;

            case KeyNotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Message = "Resource not found";
                break;

            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        if (_env.IsDevelopment())
        {
            response.Message = $"{response.Message}\nDetails: {exception.Message}";
            if (exception.StackTrace != null)
            {
                response.Message += $"\nStack Trace: {exception.StackTrace}";
            }
        }

        var result = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            WriteIndented = _env.IsDevelopment()
        });
        
        await context.Response.WriteAsync(result);
    }
}

/// <summary>
/// Extension method to add the exception handling middleware to the application pipeline
/// </summary>
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}

/// <summary>
/// Represents a validation error
/// </summary>
public class ValidationError
{
    /// <summary>
    /// Gets or sets the name of the property that caused the validation error
    /// </summary>
    public string PropertyName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the error message
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value that was attempted to be used
    /// </summary>
    public object? AttemptedValue { get; set; }
} 