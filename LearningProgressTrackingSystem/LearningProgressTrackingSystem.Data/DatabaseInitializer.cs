using Microsoft.EntityFrameworkCore;

namespace LearningProgressTrackingSystem.Data;

public static class DatabaseInitializer
{
    public static void Initialize(LearningProgressTrackingSystemContext context)
    {
        try
        {
            context.Database.Migrate();
            Console.WriteLine("Database initialization completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
            throw;
        }
    }
}