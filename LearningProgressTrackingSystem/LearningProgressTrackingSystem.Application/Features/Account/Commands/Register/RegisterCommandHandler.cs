using System.Net;
using LearningProgressTrackingSystem.Application.Common;
using LearningProgressTrackingSystem.Domain.Contracts;
using LearningProgressTrackingSystem.Domain.Entities;
using LearningProgressTrackingSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LearningProgressTrackingSystem.Application.Features.Account.Commands.Register;

public sealed class RegisterCommandHandler(
    IAccountRepository accountRepository,
    IPasswordHasher<AccountEntity> passwordHasher) : IRequestHandler<RegisterCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        AccountEntity? account = await accountRepository.GetByLoginAsNoTrackingAsync(request.Login, cancellationToken);

        if (account is not null)
        {
            return Result<Unit>.Failure($"Account with login {request.Login} already exists");
        }
        
        string hashedPassword = passwordHasher.HashPassword(null, request.Password);

        AccountEntity newAccount = new()
        {
            Login = request.Login,
            Password = hashedPassword,
            Role = AccountRole.Teacher
        };
        
        await accountRepository.AddAsync(newAccount, cancellationToken);

        return Result<Unit>.Success(Unit.Value, HttpStatusCode.Created);
    }
}