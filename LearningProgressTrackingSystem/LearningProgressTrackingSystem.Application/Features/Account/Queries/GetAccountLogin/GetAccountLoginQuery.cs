using LearningProgressTrackingSystem.Application.Features.Account.DTOs;
using LearningProgressTrackingSystem.Application.Models;
using MediatR;

namespace LearningProgressTrackingSystem.Application.Features.Account.Queries.GetAccountLogin;

public sealed record GetAccountLoginQuery(int AccountId) : IRequest<Result<AccountLoginDto>>;