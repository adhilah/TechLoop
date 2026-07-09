using MediatR;
using TechLoop.Application.Features.Topics.DTOs;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.Topics.Queries.GetAllTopics;

public sealed class GetAllTopicQueryHandler : IRequestHandler<GetAllTopicQuery, IEnumerable<TopicResponse>>
{
    private readonly ITopicsRepository _repository;
    public GetAllTopicQueryHandler(ITopicsRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<TopicResponse>> Handle(GetAllTopicQuery request, CancellationToken cancellationToken)
    {
        var topics = await _repository.GetAllAsync(cancellationToken);
        return topics.Select(topic => new TopicResponse
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
        });
    }
}