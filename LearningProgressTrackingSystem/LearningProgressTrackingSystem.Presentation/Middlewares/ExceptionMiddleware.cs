using System.Text.Json;

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
            await HandleExceptionAsync(
                context,
                ex.Message,
                StatusCodes.Status500InternalServerError
            );
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, string message, int statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        string redirectUrl =
            $"/Info/Error?message={Uri.EscapeDataString(message)}&statusCode={statusCode}";
        
        context.Response.Redirect(redirectUrl);
    }
}