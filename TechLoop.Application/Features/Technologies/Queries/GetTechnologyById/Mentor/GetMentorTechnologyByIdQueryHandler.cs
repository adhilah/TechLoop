using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.Technologies.DTOs;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.Technologies.Queries.GetTechnologyById.Mentor;

public sealed class GetMentorTechnologyByIdQueryHandler : IRequestHandler<GetMentorTechnologyByIdQuery, MentorTechnologyResponse>
{
    private readonly ITechnologyRepository _repository;
    public GetMentorTechnologyByIdQueryHandler(ITechnologyRepository repository)
    {
        _repository = repository;
    }

    public async Task<MentorTechnologyResponse> Handle(GetMentorTechnologyByIdQuery request, CancellationToken cancellationToken)
    {
        var technology = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (technology is null)
        {
            throw new NotFoundException("Technology not found.");
        }

        return new MentorTechnologyResponse
        {
            Id = technology.Id,
            Name = technology.Name,
            Slug = technology.Slug,
            Description = technology.Description,
            ImageUrl = technology.ImageUrl,
            Position = technology.Position,
            PublishedAt = technology.PublishedAt,
            PublishedBy = technology.PublishedBy,
            CreatedAt = technology.CreatedAt,
            CreatedBy = technology.CreatedBy,
            UpdatedAt = technology.UpdatedAt,
            UpdatedBy = technology.UpdatedBy
        };
    }
}