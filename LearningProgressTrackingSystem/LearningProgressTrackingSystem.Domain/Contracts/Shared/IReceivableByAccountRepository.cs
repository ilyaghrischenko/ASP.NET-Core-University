using LearningProgressTrackingSystem.Domain.Entities.Shared;

namespace LearningProgressTrackingSystem.Domain.Contracts.Shared;

public interface IReceivableByAccountRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByAccountIdAsNoTrackingAsync(int accountId, CancellationToken cancellationToken);
}