using TechLoop.Domain.Enums;

namespace TechLoop.Application.DTOs.SubTopics.Responses;

public class SubTopicResponse
{
    public int Id { get; set; }
    public int TopicId { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int Position { get; set; }
    public ContentStatus Status { get; set; }
    public DateTime? PublishedAt { get; set; }
}