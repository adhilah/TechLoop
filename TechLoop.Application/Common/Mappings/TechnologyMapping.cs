using TechLoop.Application.Features.Technologies.DTOs;
using TechLoop.Domain.Entities;

namespace TechLoop.Application.Common.Mappings;

public static class TechnologyMappings
{
    public static TechnologyResponse ToResponse(
        this Technology technology)
    {
        return new TechnologyResponse
        {
            Id = technology.Id,
            CategoryId = technology.CategoryId,
            Name = technology.Name,
            Slug = technology.Slug,
            Description = technology.Description,
            ImageUrl = technology.ImageUrl,
            Position = technology.Position,
            CreatedAt = technology.CreatedAt,
            CreatedBy = technology.CreatedBy
        };
    }
}