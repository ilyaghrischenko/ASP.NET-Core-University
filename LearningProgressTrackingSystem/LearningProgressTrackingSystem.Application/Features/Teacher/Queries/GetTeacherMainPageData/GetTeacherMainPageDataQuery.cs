using LearningProgressTrackingSystem.Application.Common;
using LearningProgressTrackingSystem.Application.Features.Common.DTOs;
using MediatR;

namespace LearningProgressTrackingSystem.Application.Features.Teacher.Queries.GetTeacherMainPageData;

public sealed record GetTeacherMainPageDataQuery(int AccountId) : IRequest<Result<MainPageDto>>;