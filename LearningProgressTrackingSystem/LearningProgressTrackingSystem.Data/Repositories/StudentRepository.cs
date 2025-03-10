using LearningProgressTrackingSystem.Data.Repositories.Shared;
using LearningProgressTrackingSystem.Domain.Contracts;
using LearningProgressTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningProgressTrackingSystem.Data.Repositories;

public class StudentRepository(LearningProgressTrackingSystemContext context) 
    : BaseRepository<Student>(context), IStudentRepository
{
    protected override IQueryable<Student> ApplyIncludes(IQueryable<Student> dbSet)
    {
        return dbSet
            .Include(student => student.Courses)
            .Include(student => student.Grades)
            .Include(student => student.Assignments);
    }

    public async Task<Student?> GetByIdAsNoTrackingAsync(int id, CancellationToken ct)
    {
        var dbSet = context.Students;
        var query = ApplyIncludes(dbSet);
        
        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(student => student.Id == id, ct);
    }

    public async Task<IEnumerable<Student>?> GetAllAsNoTrackingAsync(CancellationToken ct)
    {
        var dbSet = context.Students;
        var query = ApplyIncludes(dbSet);
        
        return await query
            .AsNoTracking()
            .ToListAsync(ct);
    }
}