using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LearningProgressTrackingSystem.Data;

public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LearningProgressTrackingSystemContext>
{
    public LearningProgressTrackingSystemContext CreateDbContext(string[] args)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string relativePath = "../LearningProgressTrackingSystem.Presentation";
        
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(currentDirectory, relativePath))
            .AddJsonFile("appsettings.json")
            .Build();

        string? connectionString = configuration.GetConnectionString("Local");

        var optionsBuilder = new DbContextOptionsBuilder<LearningProgressTrackingSystemContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new LearningProgressTrackingSystemContext(optionsBuilder.Options);
    }
}