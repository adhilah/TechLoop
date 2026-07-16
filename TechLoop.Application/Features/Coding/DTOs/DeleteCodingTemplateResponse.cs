namespace TechLoop.Application.Features.Coding.DTOs;

public sealed class DeleteCodingTemplateResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}