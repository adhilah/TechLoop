namespace TechLoop.Application.Features.Questions.DTOs;

public sealed class DeleteQuestionResponse
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;
}