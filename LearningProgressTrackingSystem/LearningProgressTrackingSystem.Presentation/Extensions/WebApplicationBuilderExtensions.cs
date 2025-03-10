using LearningProgressTrackingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace LearningProgressTrackingSystem.Presentation.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddPooledDbContextFactory(this IHostApplicationBuilder builder)
    {
        string? connectionString = builder.Configuration.GetConnectionString("Local");
        
        builder.Services.AddDbContext<LearningProgressTrackingSystemContext>(options =>
            options.UseNpgsql(connectionString));
                
        return builder;
    }
    
    public static IHostApplicationBuilder AddRepositories(this IHostApplicationBuilder builder)
    {
        

        return builder;
    }
    
    public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
    {
        
        
        return builder;
    }
    
    public static IHostApplicationBuilder AddIntegrationServices(this IHostApplicationBuilder builder)
    {
        
        
        return builder;
    }
}