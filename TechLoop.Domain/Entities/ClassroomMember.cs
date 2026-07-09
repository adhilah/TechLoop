using TechLoop.Domain.Enums;

namespace TechLoop.Domain.Entities;

public sealed class ClassroomMember
{
    public int Id { get; set; }

    public int ClassroomId { get; set; }

    public Guid UserId { get; set; }

    public ClassroomMemberRole Role { get; set; }

    public DateTime JoinedAt { get; set; }
}