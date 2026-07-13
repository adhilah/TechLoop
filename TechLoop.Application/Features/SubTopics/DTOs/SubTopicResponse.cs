using TechLoop.Domain.Entities;

namespace TechLoop.Application.Features.SubTopics.DTOs;

public sealed class SubTopicResponse
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public IEnumerable<SubTopic>? SubTopics { get; set; }

    public SubTopic? SubTopic { get; set; }
}