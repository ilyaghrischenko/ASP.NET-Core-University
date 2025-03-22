using System.Net;
using FluentValidation;

namespace LearningProgressTrackingSystem.Presentation.Middlewares;

public sealed class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            HandleException(
                context,
                ex.Message,
                StatusCodes.Status500InternalServerError
            );
        }
    }
    
    private void HandleException(HttpContext context, string message, int statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        string redirectUrl =
            $"/Info/Error?message={Uri.EscapeDataString(message)}&statusCode={statusCode}";
        
        context.Response.Redirect(redirectUrl);
    }
}