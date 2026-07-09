namespace TechLoop.Domain.Entities;

public sealed class Channel
{
    public int Id { get; set; }

    public int ClassroomId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int Position { get; set; }

    public DateTime? PublishedAt { get; set; }

    public Guid? PublishedBy { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}