using LearningProgressTrackingSystem.Domain.Entities.Shared;

namespace LearningProgressTrackingSystem.Domain.Entities;

public sealed class Grade : BaseEntity
{
    public required uint Score { get; set; }
    public required DateOnly SetAt { get; set; }

    public required Student Student { get; set; }
    public required Assignment Assignment { get; set; }
    public required Course Course { get; set; }
}