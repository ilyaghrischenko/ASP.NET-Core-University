using LearningProgressTrackingSystem.Data.Repositories.Shared;
using LearningProgressTrackingSystem.Domain.Contracts;
using LearningProgressTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningProgressTrackingSystem.Data.Repositories;

public sealed class AccountRepository(LearningProgressTrackingSystemContext context) 
    : BaseRepository<AccountEntity>(context), IAccountRepository
{
    private readonly LearningProgressTrackingSystemContext _context = context;
    
    public async Task<AccountEntity?> GetByIdAsNoTrackingAsync(int id, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        var dbSet = _context.Accounts;
        
        return await dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<IEnumerable<AccountEntity>?> GetAllAsNoTrackingAsync(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var dbSet = _context.Accounts;
        
        return await dbSet
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<AccountEntity?> GetByLoginAsNoTrackingAsync(string login, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var dbSet = _context.Accounts;
        
        return await dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Login == login, ct);
    }
}