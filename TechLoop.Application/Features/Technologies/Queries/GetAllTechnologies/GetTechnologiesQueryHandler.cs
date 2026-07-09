using TechLoop.Application.Features.Technologies.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;
using MediatR;
using TechLoop.Application.Common.Exceptions;

namespace TechLoop.Application.Features.Technologies.Queries.GetAllTechnologies;
public sealed class GetAllTechnologiesQueryHandler
    : IRequestHandler<GetAllTechnologiesQuery, List<TechnologyResponse>>
{
    private readonly ITechnologyRepository _technologyRepository;

    public GetAllTechnologiesQueryHandler(
        ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;
    }

    public async Task<List<TechnologyResponse>> Handle(
        GetAllTechnologiesQuery request,
        CancellationToken cancellationToken)
    {
        var technologies = await _technologyRepository.GetAllAsync(cancellationToken);

        return technologies.Select(x => new TechnologyResponse()
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Name = x.Name,
                Slug = x.Slug,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Position = x.Position,
                Status = x.Status
            })
            .ToList();
    }
}