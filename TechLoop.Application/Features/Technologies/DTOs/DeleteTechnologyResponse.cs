namespace TechLoop.Application.Features.Technologies.DTOs;

public sealed record DeleteTechnologyResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}