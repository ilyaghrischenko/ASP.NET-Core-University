using LearningProgressTrackingSystem.Data.Repositories.Shared;
using LearningProgressTrackingSystem.Domain.Contracts;
using LearningProgressTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningProgressTrackingSystem.Data.Repositories;

public sealed class StudentRepository(LearningProgressTrackingSystemContext context) 
    : BaseRepository<Student>(context), IStudentRepository
{
    private readonly LearningProgressTrackingSystemContext _context = context;

    protected override IQueryable<Student> ApplyIncludes(IQueryable<Student> dbSet)
    {
        return dbSet
            .Include(student => student.Account)
            .Include(student => student.Courses)
            .Include(student => student.Grades)
            .Include(student => student.Assignments);
    }

    public async Task<Student?> GetByIdAsNoTrackingAsync(int id, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var dbSet = _context.Students;
        var query = ApplyIncludes(dbSet);
        
        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(student => student.Id == id, ct);
    }

    public async Task<IEnumerable<Student>?> GetAllAsNoTrackingAsync(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var dbSet = _context.Students;
        var query = ApplyIncludes(dbSet);
        
        return await query
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<Student?> GetByAccountIdAsNoTrackingAsync(int accountId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var dbSet = _context.Students;
        var query = ApplyIncludes(dbSet);
        
        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(student => student.Account != null && student.Account.Id == accountId,
                cancellationToken);
    }
}