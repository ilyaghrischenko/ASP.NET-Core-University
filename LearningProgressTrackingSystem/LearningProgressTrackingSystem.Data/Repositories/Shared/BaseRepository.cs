using LearningProgressTrackingSystem.Domain.Contracts.Shared;
using LearningProgressTrackingSystem.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;

namespace LearningProgressTrackingSystem.Data.Repositories.Shared;

public class BaseRepository<TEntity>(LearningProgressTrackingSystemContext context)
    : IRepository<TEntity> where TEntity : BaseEntity
{
    protected virtual IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> dbSet)
    {
        return dbSet;
    }
    
    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        var dbSet = context.Set<TEntity>();
        var query = ApplyIncludes(dbSet);

        return await query
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task AddAsync(TEntity entity, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        var dbSet = context.Set<TEntity>();
        
        await dbSet.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(TEntity entity, Action updateAction, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        var dbSet = context.Set<TEntity>();

        dbSet.Update(entity);
        updateAction();
        
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        var dbSet = context.Set<TEntity>();
        
        dbSet.Remove(entity);
        await context.SaveChangesAsync(ct);
    }
}