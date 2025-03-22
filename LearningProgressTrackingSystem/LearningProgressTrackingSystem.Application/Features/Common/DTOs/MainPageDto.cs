using LearningProgressTrackingSystem.Application.Features.Account.DTOs;
using LearningProgressTrackingSystem.Application.Features.Course.DTOs;

namespace LearningProgressTrackingSystem.Application.Features.Common.DTOs;

public sealed record MainPageDto(AccountLoginDto AccountLoginDto, List<CourseDto> Courses);
