using LearningProgressTrackingSystem.Domain.Contracts.Shared;
using LearningProgressTrackingSystem.Domain.Entities;

namespace LearningProgressTrackingSystem.Domain.Contracts;

public interface IStudentRepository
    : IRepository<Student>, IReceivableRepository<Student>
{
    
}