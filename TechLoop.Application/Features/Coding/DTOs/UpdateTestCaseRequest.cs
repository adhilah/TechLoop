namespace TechLoop.Application.Features.Coding.DTOs;

public sealed class UpdateTestCaseRequest
{
    public string Input { get; set; } = string.Empty;
    public string ExpectedOutput { get; set; } = string.Empty;
    public bool IsHidden { get; set; }
    public int Position { get; set; }
}