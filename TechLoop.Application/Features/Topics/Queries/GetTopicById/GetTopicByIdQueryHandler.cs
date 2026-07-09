using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.Topics.DTOs;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.Topics.Queries.GetTopicById;

public sealed class GetTopicByIdQueryHandler
    : IRequestHandler<GetTopicByIdQuery, TopicResponse>
{
    private readonly ITopicsRepository _repository;

    public GetTopicByIdQueryHandler(ITopicsRepository repository)
    {
        _repository = repository;
    }

    public async Task<TopicResponse> Handle(
        GetTopicByIdQuery request,
        CancellationToken cancellationToken)
    {
        var topic = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (topic is null)
        {
            throw new NotFoundException("Topic not found.");
        }

        return new TopicResponse
        {
            Id = topic.Id,
            TechnologyId = topic.TechnologyId,
            Title = topic.Title,
            Slug = topic.Slug,
            Description = topic.Description,
            ImageUrl = topic.ImageUrl,
            Position = topic.Position,
            Status = topic.Status,
            PublishedAt = topic.PublishedAt,
            PublishedBy = topic.PublishedBy,
            CreatedAt = topic.CreatedAt,
            CreatedBy = topic.CreatedBy,
            UpdatedAt = topic.UpdatedAt,
            UpdatedBy = topic.UpdatedBy
        };
    }
}