using LearningProgressTrackingSystem.Data;
using LearningProgressTrackingSystem.Domain.Entities;
using LearningProgressTrackingSystem.Presentation.Extensions;
using LearningProgressTrackingSystem.Presentation.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder
    .AddDbContext()
    .AddRepositories()
    .AddApplicationServices()
    .AddIntegrationServices()
    .AddMediatR()
    .AddOptions()
    .AddJwtBearerAuthentication()
    .AddResponseCompression();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Info/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=LogIn}/{id?}")
    .WithStaticAssets();

app.UseResponseCompression();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowMvcClient");

#region DatabaseInitializer
using (var scope = app.Services.CreateScope())
{
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
#endregion

app.Run();