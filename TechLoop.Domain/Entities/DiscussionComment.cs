namespace TechLoop.Domain.Entities;

public sealed class DiscussionComment
{
    public int Id { get; set; }

    public int DiscussionId { get; set; }

    public Guid UserId { get; set; }

    public int? ParentCommentId { get; set; }

    public string Content { get; set; } = string.Empty;

    public bool IsPublished { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}