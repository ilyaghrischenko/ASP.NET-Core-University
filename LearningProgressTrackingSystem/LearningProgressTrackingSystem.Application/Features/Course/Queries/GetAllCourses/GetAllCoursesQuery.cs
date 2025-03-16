using MediatR;

namespace LearningProgressTrackingSystem.Application.Features.Course.Queries.GetAllCourses;

public sealed record GetAllCoursesQuery() : IRequest<List<Domain.Entities.Course>>;