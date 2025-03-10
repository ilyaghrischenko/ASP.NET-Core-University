using LearningProgressTrackingSystem.Domain.Contracts.Shared;
using LearningProgressTrackingSystem.Domain.Entities;

namespace LearningProgressTrackingSystem.Domain.Contracts;

public interface ITeacherRepository
    : IRepository<Teacher>, IReceivableRepository<Teacher>
{
    
}