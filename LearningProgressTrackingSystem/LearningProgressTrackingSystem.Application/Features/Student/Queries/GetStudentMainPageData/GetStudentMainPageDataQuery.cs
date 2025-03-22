using LearningProgressTrackingSystem.Application.Common;
using LearningProgressTrackingSystem.Application.Features.Common.DTOs;
using MediatR;

namespace LearningProgressTrackingSystem.Application.Features.Student.Queries.GetStudentMainPageData;

public sealed record GetStudentMainPageDataQuery(int AccountId) : IRequest<Result<MainPageDto>>;