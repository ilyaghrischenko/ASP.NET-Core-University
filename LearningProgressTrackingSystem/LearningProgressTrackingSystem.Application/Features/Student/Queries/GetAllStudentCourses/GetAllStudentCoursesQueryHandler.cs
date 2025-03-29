using System.Net;
using LearningProgressTrackingSystem.Application.Common;
using LearningProgressTrackingSystem.Application.Features.Course.DTOs;
using LearningProgressTrackingSystem.Domain.Contracts;
using MediatR;

namespace LearningProgressTrackingSystem.Application.Features.Student.Queries.GetAllStudentCourses;

public sealed class GetAllStudentCoursesQueryHandler(
    ICourseRepository courseRepository,
    IStudentRepository studentRepository)
    : IRequestHandler<GetAllStudentCoursesQuery, Result<List<CourseDto>>>
{
    public async Task<Result<List<CourseDto>>> Handle(
        GetAllStudentCoursesQuery request, CancellationToken cancellationToken)
    {
        var student = await studentRepository.GetByAccountIdAsNoTrackingAsync(request.AccountId, cancellationToken);

        if (student == null)
        {
            return Result<List<CourseDto>>.Failure("No student found", HttpStatusCode.Unauthorized);
        }
        
        var courses = await courseRepository
            .GetAllAsNoTrackingAsync(cancellationToken);
        
        if (courses == null || !courses.Any())
        {
            return Result<List<CourseDto>>.Success([]);
        }
        
        var coursesDto = courses
            .Where(course => course.Students.Contains(student))
            .Select(course => new CourseDto(
                course.Id,
                course.Title,
                course.Description,
                course.Teacher != null ? course.Teacher.Name : "No Teacher"
            ))
            .ToList();

        return Result<List<CourseDto>>.Success(coursesDto);
    }
}