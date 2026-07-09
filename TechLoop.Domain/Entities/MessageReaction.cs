namespace TechLoop.Domain.Entities;
public class MessageReaction
{
    public int Id { get; set; }

    public int MessageId { get; set; }

    public int UserId { get; set; }

    public string Reaction { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }
}