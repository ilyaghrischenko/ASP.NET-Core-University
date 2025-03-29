using LearningProgressTrackingSystem.Data.Repositories.Shared;
using LearningProgressTrackingSystem.Domain.Contracts;
using LearningProgressTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningProgressTrackingSystem.Data.Repositories;

public sealed class TeacherRepository(LearningProgressTrackingSystemContext context)
    : BaseRepository<Teacher>(context), ITeacherRepository
{
    private readonly LearningProgressTrackingSystemContext _context = context;

    protected override IQueryable<Teacher> ApplyIncludes(IQueryable<Teacher> dbSet)
    {
        return dbSet
            .Include(teacher => teacher.Account)
            .Include(teacher => teacher.Courses);
    }

    public async Task<Teacher?> GetByIdAsNoTrackingAsync(int id, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var dbSet = _context.Teachers;
        var query = ApplyIncludes(dbSet);
        
        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(teacher => teacher.Id == id, ct);
    }

    public async Task<IEnumerable<Teacher>?> GetAllAsNoTrackingAsync(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var dbSet = _context.Teachers;
        var query = ApplyIncludes(dbSet);
        
        return await query
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<Teacher?> GetByAccountIdAsNoTrackingAsync(int accountId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var dbSet = _context.Teachers;
        var query = ApplyIncludes(dbSet);
        
        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(teacher => teacher.Account != null && teacher.Account.Id == accountId,
                cancellationToken);
    }
}