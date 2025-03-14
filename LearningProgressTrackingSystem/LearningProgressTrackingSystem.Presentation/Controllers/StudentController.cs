using LearningProgressTrackingSystem.Application.DTO.Requests.Account.Queries;
using LearningProgressTrackingSystem.Application.DTO.Responses.Account;
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
            Result<AccountLoginResponse> response = await mediator.Send(query);

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
