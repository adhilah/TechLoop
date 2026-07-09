namespace TechLoop.Application.Features.Technologies.DTOs;

public sealed class UpdateTechnologyResponse
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;
}