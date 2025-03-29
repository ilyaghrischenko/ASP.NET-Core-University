using LearningProgressTrackingSystem.Application.Common;
using LearningProgressTrackingSystem.Application.Features.Course.DTOs;
using MediatR;

namespace LearningProgressTrackingSystem.Application.Features.Student.Queries.GetAllStudentCourses;

public sealed record GetAllStudentCoursesQuery(int AccountId) : IRequest<Result<List<CourseDto>>>;