namespace LearningProgressTrackingSystem.Application.Options;

public sealed class CookieSettings
{
    public required bool HttpOnly { get; init; }
    public required bool Secure { get; init; }
    public required string SameSite { get; init; }
    public required int ExpiresInHours { get; init; }
}