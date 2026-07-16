namespace TechLoop.Application.Features.Coding.DTOs;

public sealed class LearnerTestCaseResponse
{
    public int Id { get; set; }
    public string Input { get; set; } = string.Empty;
    public string ExpectedOutput { get; set; } = string.Empty;
    public int Position { get; set; }
}