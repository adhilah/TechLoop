using MediatR;
using TechLoop.Application.Features.SubTopics.DTOs;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.SubTopics.Queries.GetAllSubTopics;

public sealed class GetAllSubTopicsQueryHandler
    : IRequestHandler<GetAllSubTopicsQuery, SubTopicResponse>
{
    private readonly ISubTopicsRepository _repository;

    public GetAllSubTopicsQueryHandler(ISubTopicsRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubTopicResponse> Handle(GetAllSubTopicsQuery request, CancellationToken cancellationToken)
    {
        var subTopics = await _repository.GetAllAsync(cancellationToken);

        return new SubTopicResponse
        {
            Success = true,
            Message = "Sub topics retrieved successfully.",
            SubTopics = subTopics
        };
    }
}