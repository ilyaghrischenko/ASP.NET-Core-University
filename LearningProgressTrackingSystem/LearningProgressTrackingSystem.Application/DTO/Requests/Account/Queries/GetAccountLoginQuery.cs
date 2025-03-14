using LearningProgressTrackingSystem.Application.DTO.Responses.Account;
using LearningProgressTrackingSystem.Application.Models;
using LearningProgressTrackingSystem.Domain.Entities;
using MediatR;

namespace LearningProgressTrackingSystem.Application.DTO.Requests.Account.Queries;

public sealed record GetAccountLoginQuery(int AccountId) : IRequest<Result<AccountLoginResponse>>;