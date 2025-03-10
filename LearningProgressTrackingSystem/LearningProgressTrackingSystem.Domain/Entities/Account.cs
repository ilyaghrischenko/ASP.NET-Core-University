using LearningProgressTrackingSystem.Domain.Entities.Shared;

namespace LearningProgressTrackingSystem.Domain.Entities;

public class Account : BaseEntity
{
    public required string Login { get; set; }
    public required string Password { get; set; }
}