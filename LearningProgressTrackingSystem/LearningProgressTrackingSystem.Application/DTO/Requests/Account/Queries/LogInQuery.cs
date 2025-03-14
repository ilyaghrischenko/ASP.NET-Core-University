using LearningProgressTrackingSystem.Application.Models;
using LearningProgressTrackingSystem.Domain.Entities;
using LearningProgressTrackingSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LearningProgressTrackingSystem.Application.DTO.Requests.Account.Queries;

public sealed record LogInQuery(string Login, string Password) : IRequest<Result<AccountEntity>>;