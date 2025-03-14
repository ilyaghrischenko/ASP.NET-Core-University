using System.Net;

namespace LearningProgressTrackingSystem.Application.DTO.Responses.Info;

public sealed record ErrorResponse(string Message, int StatusCode);