using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.SubTopics.DTOs;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.SubTopics.Queries.GetSubTopicById;

public sealed class GetSubTopicByIdQueryHandler
    : IRequestHandler<GetSubTopicByIdQuery, SubTopicResponse>
{
    private readonly ISubTopicsRepository _repository;

    public GetSubTopicByIdQueryHandler(ISubTopicsRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubTopicResponse> Handle(
        GetSubTopicByIdQuery request,
        CancellationToken cancellationToken)
    {
        var subTopic = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (subTopic is null)
        {
            throw new NotFoundException("Sub topic not found.");
        }
        
        var subTopics = await _repository.GetAllAsync(cancellationToken);

        return new SubTopicResponse
        {
            Success = true,
            Message = "Sub topic retrieved successfully.",
            SubTopics = subTopics
        };
    }
}