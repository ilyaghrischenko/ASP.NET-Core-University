using LearningProgressTrackingSystem.Data.Repositories.Shared;
using LearningProgressTrackingSystem.Domain.Contracts;
using LearningProgressTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningProgressTrackingSystem.Data.Repositories;

public sealed class CourseRepository(LearningProgressTrackingSystemContext context) 
    : BaseRepository<Course>(context), ICourseRepository
{
    private readonly LearningProgressTrackingSystemContext _context = context;

    protected override IQueryable<Course> ApplyIncludes(IQueryable<Course> dbSet)
    {
        return dbSet
            .Include(course => course.Teacher)
            .Include(course => course.Students)
            .Include(course => course.Assignments);
    }

    public async Task<Course?> GetByIdAsNoTrackingAsync(int id, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        var dbSet = ApplyIncludes(_context.Courses);
        
        return await dbSet
            .AsNoTracking() 
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<IEnumerable<Course>?> GetAllAsNoTrackingAsync(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var dbSet = ApplyIncludes(_context.Courses);
        
        return await dbSet
            .AsNoTracking()
            .ToListAsync(ct);
    }
}