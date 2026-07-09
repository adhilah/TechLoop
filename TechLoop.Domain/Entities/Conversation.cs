using TechLoop.Domain.Enums;
namespace TechLoop.Domain.Entities;

public sealed class Conversation
{
    public int Id { get; set; }

    public Guid LearnerId { get; set; }

    public Guid? MentorId { get; set; }

    public int? TechnologyCategoryId { get; set; }

    public ConversationType ConversationType { get; set; }

    public ConversationStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}