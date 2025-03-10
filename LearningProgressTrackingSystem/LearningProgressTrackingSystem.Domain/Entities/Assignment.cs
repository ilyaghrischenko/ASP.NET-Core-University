using LearningProgressTrackingSystem.Domain.Entities.Shared;

namespace LearningProgressTrackingSystem.Domain.Entities;

public class Assignment : BaseEntity
{
    public required string Title { get; set; }
    public required DateOnly Deadline { get; set; }
    public required string Description { get; set; }
    
    public required Course Course { get; set; }
    
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}