using FluentValidation;
using LearningProgressTrackingSystem.Application.Features.Account.Commands.Register;

namespace LearningProgressTrackingSystem.Application.Features.Account.Validators;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(command => command.Login)
            .NotEmpty().WithMessage("Login field is required.")
            .MinimumLength(4).WithMessage("Login must be minimum 4 characters.")
            .MaximumLength(20).WithMessage("Login must be maximum 20 characters.");
        
        RuleFor(command => command.Password)
            .NotEmpty().WithMessage("Password field is required.")
            .MinimumLength(4).WithMessage("Password must be minimum 4 characters.")
            .MaximumLength(20).WithMessage("Password must be maximum 20 characters.");
        
        RuleFor(command => command.ConfirmPassword)
            .Equal(command => command.Password).WithMessage("Confirm password does not match.");
    }
}