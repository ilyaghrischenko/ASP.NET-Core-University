using LearningProgressTrackingSystem.Domain.Entities.Shared;

namespace LearningProgressTrackingSystem.Domain.Entities;

public sealed class Teacher : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    
    public AccountEntity? Account { get; set; }
    
    public ICollection<Course> Courses { get; set; } = new List<Course>();

    public Teacher() { }
}