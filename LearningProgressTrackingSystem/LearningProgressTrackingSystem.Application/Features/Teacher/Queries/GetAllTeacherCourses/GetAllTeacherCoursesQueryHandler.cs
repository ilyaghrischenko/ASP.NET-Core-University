using System.Net;
using LearningProgressTrackingSystem.Application.Common;
using LearningProgressTrackingSystem.Application.Features.Course.DTOs;
using LearningProgressTrackingSystem.Domain.Contracts;
using MediatR;

namespace LearningProgressTrackingSystem.Application.Features.Teacher.Queries.GetAllTeacherCourses;

public sealed class GetAllTeacherCoursesQueryHandler(
    ICourseRepository courseRepository,
    ITeacherRepository teacherRepository)
    : IRequestHandler<GetAllTeacherCoursesQuery, Result<List<CourseDto>>>
{
    public async Task<Result<List<CourseDto>>> Handle(
        GetAllTeacherCoursesQuery request, CancellationToken cancellationToken)
    {
        var teacher = await teacherRepository.GetByAccountIdAsNoTrackingAsync(request.AccountId, cancellationToken);

        if (teacher == null)
        {
            return Result<List<CourseDto>>.Failure("Teacher not found", HttpStatusCode.Unauthorized);
        }
        
        var courses = await courseRepository
            .GetAllAsNoTrackingAsync(cancellationToken);
        
        if (courses == null || !courses.Any())
        {
            return Result<List<CourseDto>>.Success([]);
        }

        var coursesDto = courses
            .Where(course =>
            {
                if (course.Teacher == null)
                {
                    return false;
                }

                return course.Teacher.Id == teacher.Id;
            })
            .Select(course => new CourseDto(
                course.Id,
                course.Title,
                course.Description,
                course.Teacher!.Name
            ))
            .ToList();
        
        return Result<List<CourseDto>>.Success(coursesDto);
    }
}