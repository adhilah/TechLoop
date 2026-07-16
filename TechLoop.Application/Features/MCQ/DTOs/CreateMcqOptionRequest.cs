using TechLoop.Domain.Entities;

namespace TechLoop.Application.Features.MCQ.DTOs;

public class CreateMcqOptionRequest
{
   // public int QuestionId { get; set; }
    public string OptionText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public int Position { get; set; }
}
