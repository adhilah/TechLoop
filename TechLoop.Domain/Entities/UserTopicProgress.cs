namespace TechLoop.Domain.Entities;
public class UserTopicProgress
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public int TopicId { get; set; }

    public int CompletedQuestion { get; set; }

    public DateTime? LastPracticedQuestions{ get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}