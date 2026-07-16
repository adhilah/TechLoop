namespace TechLoop.Application.Features.Coding.DTOs;

public sealed class LearnerCodingTemplateResponse
{
    public int Id { get; set; }
    public int TechnologyId { get; set; }
    public string StarterCode { get; set; } = string.Empty;
}