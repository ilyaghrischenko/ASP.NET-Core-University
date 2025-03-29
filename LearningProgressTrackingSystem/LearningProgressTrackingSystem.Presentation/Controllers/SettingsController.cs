using LearningProgressTrackingSystem.Application.Options;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LearningProgressTrackingSystem.Presentation.Controllers;

public class SettingsController(IOptionsMonitor<CookieSettings> cookiesOptions) : Controller
{
    private readonly CookieSettings _cookieSettings = cookiesOptions.CurrentValue;
    
    [HttpGet]
    public IActionResult ChangeLanguage(string culture)
    {
        var cultureInfo = string.IsNullOrEmpty(culture) ? "uk" : culture;

        var httpOnlyCookieOptions = new CookieOptions
        {
            HttpOnly = _cookieSettings.HttpOnly,
            Secure = _cookieSettings.Secure,
            SameSite = Enum.Parse<SameSiteMode>(_cookieSettings.SameSite),
            Expires = DateTimeOffset.UtcNow.AddYears(1)
        };
        
        Response.Cookies.Append(
            "Culture",
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureInfo)),
            httpOnlyCookieOptions
        );
            
        return Redirect(Request.Headers.Referer.ToString());
    }
}