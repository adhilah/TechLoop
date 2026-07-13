using TechLoop.Domain.Enums;

namespace TechLoop.Application.Features.Technologies.DTOs;

public sealed class TechnologyResponse
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int Position { get; set; }
    public ContentStatus Status { get; set; }

    public DateTime? PublishedAt { get; set; }
    public Guid? PublishedBy { get; set; }

    public DateTime CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
}