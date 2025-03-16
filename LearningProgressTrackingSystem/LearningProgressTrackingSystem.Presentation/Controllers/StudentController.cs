using LearningProgressTrackingSystem.Application.Features.Account.DTOs;
using LearningProgressTrackingSystem.Application.Features.Account.Queries.GetAccountLogin;
using LearningProgressTrackingSystem.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
            GetAccountLoginQuery query = new(accountId);
            Result<AccountLoginDto> response = await mediator.Send(query);

            return response.Map<IActionResult>(
                onSuccess: View,
                onFailure: errorMessage => RedirectToAction(
                    "Error", 
                    "Info",
                    new { message = errorMessage, statusCode = response.StatusCode }
                )
            );
        }
    }
}
