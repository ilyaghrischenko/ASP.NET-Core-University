using LearningProgressTrackingSystem.Domain.Entities.Shared;

namespace LearningProgressTrackingSystem.Domain.Entities;

public sealed class Student : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required DateOnly EnrollmentDate { get; set; }
    
    public AccountEntity? Account { get; set; }

    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public Student() { }
}