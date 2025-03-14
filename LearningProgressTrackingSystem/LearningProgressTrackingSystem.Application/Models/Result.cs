using System.Net;
using Microsoft.AspNetCore.Http;

namespace LearningProgressTrackingSystem.Application.Models;

public sealed class Result<T>
{
    public T? Value { get; }
    public string? ErrorMessage { get; }
    public bool IsSuccess => ErrorMessage == null;
    public HttpStatusCode StatusCode { get; }

    private Result(T value, HttpStatusCode statusCode)
    {
        Value = value;
        ErrorMessage = null;
        StatusCode = statusCode;
    }

    private Result(string errorMessage, HttpStatusCode statusCode)
    {
        Value = default;
        ErrorMessage = errorMessage;
        StatusCode = statusCode;
    }

    public static Result<T> Success(T value, HttpStatusCode statusCode = HttpStatusCode.OK)
        => new Result<T>(value, statusCode);
    public static Result<T> Failure(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new Result<T>(errorMessage, statusCode);

    public TResult Map<TResult>(Func<T, TResult> onSuccess, Func<string, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(Value!) : onFailure(ErrorMessage!);
    }
}