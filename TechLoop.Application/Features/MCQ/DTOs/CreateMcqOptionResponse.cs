using TechLoop.Domain.Entities;

namespace TechLoop.Application.Features.MCQ.DTOs;

public class CreateMcqOptionResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}


