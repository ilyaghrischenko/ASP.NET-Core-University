using LearningProgressTrackingSystem.Application.Common;
using LearningProgressTrackingSystem.Application.Features.Account.DTOs;
using LearningProgressTrackingSystem.Application.Features.Account.Queries.GetAccountLogin;
using LearningProgressTrackingSystem.Application.Features.Common.DTOs;
using LearningProgressTrackingSystem.Application.Features.Course.DTOs;
using LearningProgressTrackingSystem.Application.Features.Teacher.Queries.GetAllTeacherCourses;
using MediatR;

namespace LearningProgressTrackingSystem.Application.Features.Teacher.Queries.GetTeacherMainPageData;

public sealed class GetTeacherMainPageDataQueryHandler(IMediator mediator)
    : IRequestHandler<GetTeacherMainPageDataQuery, Result<MainPageDto>>
{
    public async Task<Result<MainPageDto>> Handle(
        GetTeacherMainPageDataQuery request, CancellationToken cancellationToken)
    {
        GetAccountLoginQuery loginQuery = new(request.AccountId);
        Result<AccountLoginDto> loginResponse = await mediator.Send(loginQuery, cancellationToken);
        
        if (!loginResponse.IsSuccess)
        {
            return Result<MainPageDto>.Failure(loginResponse.ErrorMessage!, loginResponse.StatusCode);
        }

        GetAllTeacherCoursesQuery coursesQuery = new(request.AccountId);
        Result<List<CourseDto>> coursesResponse = await mediator.Send(coursesQuery, cancellationToken);

        if (!coursesResponse.IsSuccess)
        {
            return Result<MainPageDto>.Failure(coursesResponse.ErrorMessage!, coursesResponse.StatusCode);
        }

        MainPageDto studentMainPageDto = new(loginResponse.Value!, coursesResponse.Value!);

        return Result<MainPageDto>.Success(studentMainPageDto);
    }
}