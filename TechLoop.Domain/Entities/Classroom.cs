namespace TechLoop.Domain.Entities;

public sealed class Classroom
{
    public int Id { get; set; }

    public int TechnologyId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsPrivate { get; set; }
    
    public Guid? PublishedBy { get; set; }

    public DateTime? PublishedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}