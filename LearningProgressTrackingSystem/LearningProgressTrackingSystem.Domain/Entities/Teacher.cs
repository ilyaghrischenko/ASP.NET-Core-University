using LearningProgressTrackingSystem.Domain.Entities.Shared;

namespace LearningProgressTrackingSystem.Domain.Entities;

public class Teacher : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    
    public Account? Account { get; set; }
    
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}