using FluentValidation;
using LearningProgressTrackingSystem.Application.Features.Account.Queries.LogIn;

namespace LearningProgressTrackingSystem.Application.Features.Account.Validators;

public sealed class LogInQueryValidator : AbstractValidator<LogInQuery>
{
    public LogInQueryValidator()
    {
        RuleFor(query => query.Login)
            .NotEmpty().WithMessage("Login field is required.")
            .MinimumLength(4).WithMessage("Login must be minimum 4 characters.")
            .MaximumLength(20).WithMessage("Login must be maximum 20 characters.");
        
        RuleFor(query => query.Password)
            .NotEmpty().WithMessage("Password field is required.")
            .MinimumLength(4).WithMessage("Password must be minimum 4 characters.")
            .MaximumLength(20).WithMessage("Password must be maximum 20 characters.");
    }
}