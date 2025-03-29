using LearningProgressTrackingSystem.Application.Common;
using LearningProgressTrackingSystem.Application.Features.Account.Commands.Register;
using LearningProgressTrackingSystem.Application.Features.Account.Queries.LogIn;
using LearningProgressTrackingSystem.Domain.Entities;
using LearningProgressTrackingSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearningProgressTrackingSystem.Presentation.Controllers
{
    public sealed class AccountController(IMediator mediator) : Controller
    {
        [HttpGet]
        public ViewResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInQuery query)
        {
            Result<AccountEntity> result = await mediator.Send(query);

            return result.Map<IActionResult>(
                onSuccess: account =>
                {
                    return account.Role switch
                    {
                        AccountRole.Student => RedirectToAction(
                            "Main", "Student", new { accountId = account.Id }),
                        AccountRole.Teacher => RedirectToAction(
                            "Main", "Teacher", new { accountId = account.Id }),
                        _ => throw new Exception("Unknown role")
                    };
                },
                onFailure: errorMessage =>
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                    return View(query);
                }
            );
        }
        
        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ViewResult> Register(RegisterCommand command)
        {
            Result<Unit> result = await mediator.Send(command);

            return result.Map<ViewResult>(
                onSuccess: _ => View("LogIn"),
                onFailure: errorMessage =>
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                    return View("Register", command);
                }
            );
        }
    }
}
