using System.Globalization;
using LearningProgressTrackingSystem.Data;
using LearningProgressTrackingSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;

namespace LearningProgressTrackingSystem.Presentation.Extensions;

public static class WebApplicationExtensions
{
    public static void InitializeDatabaseIfUninitialized(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<LearningProgressTrackingSystemContext>();
            var passwordHasher = services.GetRequiredService<IPasswordHasher<AccountEntity>>();
            DatabaseInitializer.Initialize(context, passwordHasher);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while initializing the database: {ex.Message}");
        }
    }
    
    public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
    {
        var supportedCultures = new[]
        {
            new CultureInfo("uk"),
            new CultureInfo("en"),
        };

        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("uk", "uk"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures,
            RequestCultureProviders = new List<IRequestCultureProvider>
            {
                new CookieRequestCultureProvider
                {
                    CookieName = "Culture"
                }
            }
        };
        
        app.UseRequestLocalization(localizationOptions);
        
        return app;
    }
}