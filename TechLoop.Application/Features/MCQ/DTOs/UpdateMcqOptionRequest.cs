namespace TechLoop.Application.Features.MCQ.DTOs;

public sealed class UpdateMcqOptionRequest
{
    public string OptionText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public int Position { get; set; }
}