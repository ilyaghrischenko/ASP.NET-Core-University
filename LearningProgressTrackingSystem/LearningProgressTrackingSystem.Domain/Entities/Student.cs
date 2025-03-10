using LearningProgressTrackingSystem.Domain.Entities.Shared;

namespace LearningProgressTrackingSystem.Domain.Entities;

public class Student : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required DateOnly EnrollmentDate { get; set; }
    
    public Account? Account { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
}