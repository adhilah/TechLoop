namespace TechLoop.Application.Features.SubTopics.DTOs;

public sealed class CreateSubTopicResponse
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;
}