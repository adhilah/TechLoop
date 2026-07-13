using TechLoop.Application.Features.Technologies.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;
using MediatR;
using TechLoop.Application.Common.Exceptions;

namespace TechLoop.Application.Features.Technologies.Queries.GetTechnologyById;

public sealed class GetTechnologyByIdQueryHandler
    : IRequestHandler<GetTechnologyByIdQuery, TechnologyResponse>
{
    private readonly ITechnologyRepository _technologyRepository;

    public GetTechnologyByIdQueryHandler(
        ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;
    }

    public async Task<TechnologyResponse> Handle(
        GetTechnologyByIdQuery request,
        CancellationToken cancellationToken)
    {
        var technology = await _technologyRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (technology is null)
            throw new NotFoundException("Technology not found.");

        return new TechnologyResponse
        {
            Id = technology.Id,
            CategoryId = technology.CategoryId,
            Name = technology.Name,
            Slug = technology.Slug,
            Description = technology.Description,
            ImageUrl = technology.ImageUrl,
            Position = technology.Position,
            Status = technology.Status,
            PublishedAt = technology.PublishedAt,
            PublishedBy = technology.PublishedBy,
            CreatedAt = technology.CreatedAt,
            CreatedBy = technology.CreatedBy,
            UpdatedAt = technology.UpdatedAt,
            UpdatedBy = technology.UpdatedBy,
            DeletedAt = technology.DeletedAt,
            DeletedBy = technology.DeletedBy
        };
    }
}