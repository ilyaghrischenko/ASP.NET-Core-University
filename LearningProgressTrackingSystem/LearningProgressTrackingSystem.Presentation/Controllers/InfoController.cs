using LearningProgressTrackingSystem.Application.Features.Info.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LearningProgressTrackingSystem.Presentation.Controllers
{
    public class InfoController : Controller
    {
        [HttpGet]
        public ActionResult Error(string message, int statusCode)
        {
            ErrorResponse response = new(message, statusCode);
            return View(response);
        }
    }
}
