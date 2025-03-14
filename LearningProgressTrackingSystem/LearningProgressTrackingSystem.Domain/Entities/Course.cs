using LearningProgressTrackingSystem.Domain.Entities.Shared;

namespace LearningProgressTrackingSystem.Domain.Entities;

public sealed class Course : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    
    public Teacher? Teacher { get; set; }
    
    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public Course() { }
}