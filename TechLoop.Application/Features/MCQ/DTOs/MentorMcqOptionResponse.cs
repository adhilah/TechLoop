namespace TechLoop.Application.Features.MCQ.DTOs;

public sealed class MentorMcqOptionResponse
{
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public string OptionText { get; set; } = string.Empty;

    public bool IsCorrect { get; set; }

    public int Position { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
}