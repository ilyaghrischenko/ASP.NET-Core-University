using MediatR;

namespace LearningProgressTrackingSystem.Application.Features.Course.Queries.GetAllCourses;

public sealed class GetAllCoursesQueryHandler(ICourseRepository courseRepository)
    : IRequestHandler<GetAllCoursesQuery, List<Domain.Entities.Course>>
{
    public async Task<List<Domain.Entities.Course>> Handle(
        GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await courseRepository.GetAllAsNoTrackingAsync(cancellationToken);
    }
}