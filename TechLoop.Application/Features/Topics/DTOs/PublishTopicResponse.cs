namespace TechLoop.Application.Features.Topics.DTOs;

public sealed class PublishTopicResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}