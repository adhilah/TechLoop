using TechLoop.Domain.Enums;

namespace TechLoop.Domain.Entities;
public class OneToOneMessage
{
    public int Id { get; set; }
    public string ConversationId { get; set; } = string.Empty;
    public int SenderId { get; set; }
    public SenderType SenderType { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? AttachedUrl { get; set; } =  string.Empty;
    public DateTime ReadAt { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}