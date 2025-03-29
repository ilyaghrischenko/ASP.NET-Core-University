using LearningProgressTrackingSystem.Application.Common;
using LearningProgressTrackingSystem.Application.Features.Common.DTOs;
using LearningProgressTrackingSystem.Application.Features.Teacher.Queries.GetTeacherMainPageData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearningProgressTrackingSystem.Presentation.Controllers
{
    //TODO: разобраться
    // [Authorize(Policy = "TeacherOnly")]
    public class TeacherController(IMediator mediator) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Main(int accountId)
        {
            GetTeacherMainPageDataQuery query = new(accountId);
            Result<MainPageDto> result = await mediator.Send(query);

            return result.Map<IActionResult>(
                onSuccess: View,
                onFailure: _ => RedirectToAction(
                    "LogIn",
                    "Account"
                )
            );
        }

        [HttpGet]
        public async Task<ViewResult> Students(int courseId)
        {
            //TODO
            return View();
        }
    }
}
