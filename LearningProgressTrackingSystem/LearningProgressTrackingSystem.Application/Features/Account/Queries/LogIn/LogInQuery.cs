using LearningProgressTrackingSystem.Application.Common;
using LearningProgressTrackingSystem.Domain.Entities;
using MediatR;

namespace LearningProgressTrackingSystem.Application.Features.Account.Queries.LogIn;

public sealed record LogInQuery(string Login, string Password) : IRequest<Result<AccountEntity>>;