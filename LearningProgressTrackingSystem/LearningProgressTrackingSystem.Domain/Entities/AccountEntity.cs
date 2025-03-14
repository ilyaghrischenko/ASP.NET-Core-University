using LearningProgressTrackingSystem.Domain.Entities.Shared;
using LearningProgressTrackingSystem.Domain.Enums;

namespace LearningProgressTrackingSystem.Domain.Entities;

public sealed class AccountEntity : BaseEntity
{
    public required string Login { get; set; }
    public required string Password { get; set; }
    public required AccountRole Role { get; set; }

    public AccountEntity() { }
}