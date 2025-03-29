using LearningProgressTrackingSystem.Application.Common;
using LearningProgressTrackingSystem.Application.Features.Common.DTOs;
using LearningProgressTrackingSystem.Application.Features.Student.Queries.GetStudentMainPageData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearningProgressTrackingSystem.Presentation.Controllers
{
    //TODO: разобраться
    // [Authorize(Policy = "StudentOnly")]
    public sealed class StudentController(IMediator mediator) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Main(int accountId)
        {
            GetStudentMainPageDataQuery query = new(accountId);
            Result<MainPageDto> result = await mediator.Send(query);

            return result.Map<IActionResult>(
                onSuccess: View,
                onFailure: _ => RedirectToAction(
                    "LogIn",
                    "Account"
                )
            );
        }
    }
}
