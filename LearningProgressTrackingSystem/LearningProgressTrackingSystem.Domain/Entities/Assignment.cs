using LearningProgressTrackingSystem.Domain.Entities.Shared;

namespace LearningProgressTrackingSystem.Domain.Entities;

public sealed class Assignment : BaseEntity
{
    public required string Title { get; set; }
    public required DateOnly Deadline { get; set; }
    public required string Description { get; set; }
    
    public required Course Course { get; set; }
    
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public Assignment() { }
}