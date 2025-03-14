using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningProgressTrackingSystem.Presentation.Controllers
{
    [Authorize(Policy = "TeacherOnly")]
    public class TeacherController(IMediator mediator) : Controller
    {
        [HttpGet]
        public ViewResult Main(int accountId)
        {
            return View();
        }
    }
}
