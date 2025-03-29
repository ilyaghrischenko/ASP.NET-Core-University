namespace LearningProgressTrackingSystem.Application.Features.Course.DTOs;

public sealed record CourseDto(int Id, string Title, string Description, string TeacherName = "No Teacher");