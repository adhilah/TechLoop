namespace TechLoop.Application.Features.TechnologyCategories.DTOs;

public sealed class CreateTechnologyCategoryResponse
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;
}