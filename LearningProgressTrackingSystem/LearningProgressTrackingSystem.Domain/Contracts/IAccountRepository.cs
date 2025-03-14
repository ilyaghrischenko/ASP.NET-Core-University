using LearningProgressTrackingSystem.Domain.Contracts.Shared;
using LearningProgressTrackingSystem.Domain.Entities;

namespace LearningProgressTrackingSystem.Domain.Contracts;

public interface IAccountRepository
    : IRepository<AccountEntity>, IReceivableRepository<AccountEntity>
{
    Task<AccountEntity?> GetByLoginAsNoTrackingAsync(string login, CancellationToken ct);
}