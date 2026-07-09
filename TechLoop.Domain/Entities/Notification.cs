using TechLoop.Domain.Enums;

namespace TechLoop.Domain.Entities;
public class Notification
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationType NotificationType { get; set; }
    public int ReferenceId { get; set; }
    public DateTime ReadAt { get; set; }
    public DateTime CreatedAt { get; set; }
}