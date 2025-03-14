using LearningProgressTrackingSystem.Application.Models;
using MediatR;

namespace LearningProgressTrackingSystem.Application.DTO.Requests.Account.Commands;

public sealed record RegisterCommand(string Login, string Password, string ConfirmPassword) : IRequest<Result<Unit>>;