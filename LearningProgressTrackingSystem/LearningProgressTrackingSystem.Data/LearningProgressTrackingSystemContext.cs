using LearningProgressTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningProgressTrackingSystem.Data;

public sealed class LearningProgressTrackingSystemContext : DbContext
{
    public LearningProgressTrackingSystemContext() { }

    public LearningProgressTrackingSystemContext(DbContextOptions<LearningProgressTrackingSystemContext> options)
        : base(options) { }
    
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Grade> Grades { get; set; }
}