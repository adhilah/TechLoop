namespace TechLoop.Application.Features.Topics.DTOs;

public class CreateTopicRequest
{
    public int TechnologyId { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int Position { get; set; }
}