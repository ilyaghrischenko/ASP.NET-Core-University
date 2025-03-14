using LearningProgressTrackingSystem.Presentation.Extensions;
using LearningProgressTrackingSystem.Presentation.Middlewares;

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
    app.UseExceptionHandler("/Home/Error");
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

app.Run();