using System.Net;
using LearningProgressTrackingSystem.Application.Common;
using LearningProgressTrackingSystem.Application.Features.Account.DTOs;
using LearningProgressTrackingSystem.Domain.Contracts;
using LearningProgressTrackingSystem.Domain.Entities;
using MediatR;

namespace LearningProgressTrackingSystem.Application.Features.Account.Queries.GetAccountLogin;

public sealed class GetAccountLoginQueryHandler(IAccountRepository accountRepository)
    : IRequestHandler<GetAccountLoginQuery, Result<AccountLoginDto>>
{
    public async Task<Result<AccountLoginDto>> Handle(GetAccountLoginQuery request, CancellationToken cancellationToken)
    {
        AccountEntity? account = await accountRepository.GetByIdAsNoTrackingAsync(request.AccountId, cancellationToken);

        if (account is null)
        {
            return Result<AccountLoginDto>.Failure($"Account with id: {request.AccountId} was not found", HttpStatusCode.NotFound);
        }

        AccountLoginDto dto = new(account.Login);
        return Result<AccountLoginDto>.Success(dto);
    }
}