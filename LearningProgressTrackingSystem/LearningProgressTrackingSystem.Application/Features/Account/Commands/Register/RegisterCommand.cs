using LearningProgressTrackingSystem.Application.Common;
using MediatR;

namespace LearningProgressTrackingSystem.Application.Features.Account.Commands.Register;

public sealed record RegisterCommand(string Login, string Password, string ConfirmPassword) : IRequest<Result<Unit>>;