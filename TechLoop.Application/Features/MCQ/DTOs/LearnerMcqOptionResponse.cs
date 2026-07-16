namespace TechLoop.Application.Features.MCQ.DTOs;

public sealed class LearnerMcqOptionResponse
{
    public int Id { get; set; }
    public string OptionText { get; set; } = string.Empty;
    public int Position { get; set; }
}