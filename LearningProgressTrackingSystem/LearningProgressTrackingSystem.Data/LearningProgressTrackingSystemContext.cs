using LearningProgressTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningProgressTrackingSystem.Data;

public class LearningProgressTrackingSystemContext : DbContext
{
    public LearningProgressTrackingSystemContext() { }

    public LearningProgressTrackingSystemContext(DbContextOptions<LearningProgressTrackingSystemContext> options)
        : base(options) { }
    
    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Teacher> Teachers { get; set; }
    public virtual DbSet<Assignment> Assignments { get; set; }
    public virtual DbSet<Grade> Grades { get; set; }
}