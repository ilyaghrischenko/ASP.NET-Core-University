using LearningProgressTrackingSystem.Application.Common;
using LearningProgressTrackingSystem.Application.Features.Course.DTOs;
using MediatR;

namespace LearningProgressTrackingSystem.Application.Features.Teacher.Queries.GetAllTeacherCourses;

public sealed record GetAllTeacherCoursesQuery(int AccountId) : IRequest<Result<List<CourseDto>>>;