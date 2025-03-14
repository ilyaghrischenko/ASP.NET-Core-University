using System.Net;
using LearningProgressTrackingSystem.Application.DTO.Requests.Account.Queries;
using LearningProgressTrackingSystem.Application.DTO.Responses.Account;
using LearningProgressTrackingSystem.Application.Exceptions;
using LearningProgressTrackingSystem.Application.Models;
using LearningProgressTrackingSystem.Domain.Contracts;
using LearningProgressTrackingSystem.Domain.Entities;
using MediatR;

namespace LearningProgressTrackingSystem.Application.DTO.Requests.Account.Handlers;

public sealed class GetAccountLoginQueryHandler(IAccountRepository accountRepository)
    : IRequestHandler<GetAccountLoginQuery, Result<AccountLoginResponse>>
{
    public async Task<Result<AccountLoginResponse>> Handle(GetAccountLoginQuery request, CancellationToken cancellationToken)
    {
        AccountEntity? account = await accountRepository.GetByIdAsNoTrackingAsync(request.AccountId, cancellationToken);

        if (account is null)
        {
            return Result<AccountLoginResponse>.Failure($"Account with id: {request.AccountId} was not found", HttpStatusCode.NotFound);
        }

        AccountLoginResponse response = new(account.Login);
        return Result<AccountLoginResponse>.Success(response);
    }
}