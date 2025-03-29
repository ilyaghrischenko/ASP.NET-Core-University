using LearningProgressTrackingSystem.Application.Common;
using LearningProgressTrackingSystem.Application.Features.Account.DTOs;
using MediatR;

namespace LearningProgressTrackingSystem.Application.Features.Account.Queries.GetAccountLogin;

public sealed record GetAccountLoginQuery(int AccountId) : IRequest<Result<AccountLoginDto>>;