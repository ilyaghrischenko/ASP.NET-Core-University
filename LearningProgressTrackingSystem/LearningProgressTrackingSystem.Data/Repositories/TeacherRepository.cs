using LearningProgressTrackingSystem.Data.Repositories.Shared;
using LearningProgressTrackingSystem.Domain.Contracts;
using LearningProgressTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningProgressTrackingSystem.Data.Repositories;

public class TeacherRepository(LearningProgressTrackingSystemContext context)
    : BaseRepository<Teacher>(context), ITeacherRepository
{
    private readonly LearningProgressTrackingSystemContext _context = context;

    protected override IQueryable<Teacher> ApplyIncludes(IQueryable<Teacher> dbSet)
    {
        return dbSet
            .Include(teacher => teacher.Courses);
    }

    public async Task<Teacher?> GetByIdAsNoTrackingAsync(int id, CancellationToken ct)
    {
        var dbSet = _context.Teachers;
        var query = ApplyIncludes(dbSet);
        
        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(teacher => teacher.Id == id, ct);
    }

    public async Task<IEnumerable<Teacher>?> GetAllAsNoTrackingAsync(CancellationToken ct)
    {
        var dbSet = _context.Teachers;
        var query = ApplyIncludes(dbSet);
        
        return await query
            .AsNoTracking()
            .ToListAsync(ct);
    }
}