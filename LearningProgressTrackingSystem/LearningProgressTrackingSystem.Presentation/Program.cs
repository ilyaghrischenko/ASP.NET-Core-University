using LearningProgressTrackingSystem.Data;
using LearningProgressTrackingSystem.Domain.Entities;
using LearningProgressTrackingSystem.Presentation.Extensions;
using LearningProgressTrackingSystem.Presentation.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

builder
    .AddDbContext()
    .AddRepositories()
    .AddApplicationServices()
    .AddIntegrationServices()
    .AddFluentValidation()
    .AddMediatR()
    .AddOptions()
    .AddJwtBearerAuthentication()
    .AddResponseCompression()
    .AddLocalization();

var app = builder.Build();

app.UseLocalization();

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

app.InitializeDatabaseIfUninitialized();

app.Run();