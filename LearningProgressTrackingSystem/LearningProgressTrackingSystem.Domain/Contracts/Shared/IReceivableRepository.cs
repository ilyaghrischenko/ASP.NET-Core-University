using LearningProgressTrackingSystem.Domain.Entities.Shared;

namespace LearningProgressTrackingSystem.Domain.Contracts.Shared;

public interface IReceivableRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsNoTrackingAsync(int id, CancellationToken ct);
    Task<IEnumerable<T>?> GetAllAsNoTrackingAsync(CancellationToken ct);
}