using TechLoop.Domain.Enums;

namespace TechLoop.Domain.Entities;
public class Question
{
    public int Id { get; set; }
    public int SubTopicId { get; set; }
    public QuestionType QuestionType{ get; set; } 
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public int Mark { get; set; } = 0;
    public string Hint { get; set; } = string.Empty;
    public string Explanation { get; set; } = string.Empty;
    public int? TimeLimitSeconds { get; set; } = 0;
    public int? MemoryLimitMb { get; set; } = 0;
    public DifficultyLevel Difficulty { get; set; }
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