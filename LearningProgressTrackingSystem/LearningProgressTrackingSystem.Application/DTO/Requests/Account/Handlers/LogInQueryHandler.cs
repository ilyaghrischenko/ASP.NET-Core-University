using System.Security.Claims;
using LearningProgressTrackingSystem.Application.Contracts.Identity;
using LearningProgressTrackingSystem.Application.DTO.Requests.Account.Queries;
using LearningProgressTrackingSystem.Application.Exceptions;
using LearningProgressTrackingSystem.Application.Models;
using LearningProgressTrackingSystem.Application.Options;
using LearningProgressTrackingSystem.Domain.Contracts;
using LearningProgressTrackingSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace LearningProgressTrackingSystem.Application.DTO.Requests.Account.Handlers;

public sealed class LogInQueryHandler(
    IAccountRepository accountRepository,
    IPasswordHasher<AccountEntity> passwordHasher,
    IJwtBearerService jwtService,
    IOptionsMonitor<CookieSettings> cookieSettings,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<LogInQuery, Result<AccountEntity>>
{
    private readonly CookieSettings _cookieSettings = cookieSettings.CurrentValue;
    
    public async Task<Result<AccountEntity>> Handle(LogInQuery request, CancellationToken cancellationToken)
    {
        AccountEntity? account = await accountRepository.GetByLoginAsNoTrackingAsync(request.Login, cancellationToken);

        if (account is null)
        {
            return Result<AccountEntity>.Failure(
                $"Account with login: {request.Login} not found");
        }

        PasswordVerificationResult verificationResult
            = passwordHasher.VerifyHashedPassword(account, account.Password, request.Password);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return Result<AccountEntity>.Failure(
                $"To account with login: {request.Login} provided an invalid password");
        }
        
        var response = httpContextAccessor.HttpContext?.Response;
        if (response != null)
        {
            SetTokenToHttpOnlyCookie(response, account);
        }
        
        return Result<AccountEntity>.Success(account);
    }

    private void SetTokenToHttpOnlyCookie(HttpResponse response, AccountEntity account)
    {
        ClaimsIdentity identity = jwtService.GetIdentity(account);
        string token = jwtService.GetToken(identity);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = _cookieSettings.HttpOnly,
            Secure = _cookieSettings.Secure,
            SameSite = Enum.Parse<SameSiteMode>(_cookieSettings.SameSite),
            Expires = DateTime.UtcNow.AddHours(_cookieSettings.ExpiresInHours)
        };
        
        response.Cookies.Append("Token", token, cookieOptions);
    }
}