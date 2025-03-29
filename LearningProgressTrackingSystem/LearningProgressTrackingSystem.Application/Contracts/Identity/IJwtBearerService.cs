using System.Security.Claims;
using LearningProgressTrackingSystem.Domain.Entities;

namespace LearningProgressTrackingSystem.Application.Contracts.Identity;

public interface IJwtBearerService
{
    ClaimsIdentity GetIdentity(AccountEntity account);
    string GetToken(ClaimsIdentity identity);
}