using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.SubTopics.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;

namespace TechLoop.Application.Features.SubTopics.Commands.UpdateSubTopic;

public sealed class UpdateSubTopicCommandHandler : IRequestHandler<UpdateSubTopicCommand, UpdateSubTopicResponse>
{
    private readonly ISubTopicsRepository _subtopicrepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateSubTopicCommandHandler(ISubTopicsRepository repository, ICurrentUserService currentUserService)
    {
        _subtopicrepository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<UpdateSubTopicResponse> Handle(UpdateSubTopicCommand request, CancellationToken cancellationToken)
    {
        var subTopic = await _subtopicrepository.GetByIdAsync(request.Id, cancellationToken);

        if (subTopic is null)
        {
            throw new NotFoundException("Sub topic not found.");
        }
        
        var slugExists = await _subtopicrepository.SlugExistsAsync(request.Slug, cancellationToken);
        if (slugExists && !subTopic.Slug.Equals(request.Slug, StringComparison.OrdinalIgnoreCase))
        {
            throw new ValidationException($"Topic slug '{request.Slug}' already exists.");
        }
        var positionExists = await _subtopicrepository.PositionExistsAsync(request.Position, cancellationToken);
        if (positionExists && subTopic.Position != request.Position)
        {
            throw new ValidationException($"Topic position '{request.Position}' already exists.");
        }
        var topicExists = await _subtopicrepository.TopicExistsAsync(request.TopicId, cancellationToken);

        if (!topicExists)
        {
            throw new NotFoundException("Topic not found.");
        }
        
        var exists = await _subtopicrepository.ExistsAsync(request.Title, cancellationToken);

        if (exists && !subTopic.Title.Equals(request.Title, StringComparison.OrdinalIgnoreCase))
        {
            throw new ValidationException($"Sub topic '{request.Title}' already exists.");
        }
        subTopic.TopicId = request.TopicId;
        subTopic.Title = request.Title.Trim();
        subTopic.Description = request.Description ?? string.Empty;
        subTopic.ImageUrl = request.ImageUrl ?? string.Empty;
        subTopic.Slug = request.Slug.Trim().ToLowerInvariant();
        subTopic.Position = request.Position;
        subTopic.UpdatedBy = _currentUserService.UserId;
        subTopic.UpdatedAt = DateTime.UtcNow;

        var rowsAffected = await _subtopicrepository.UpdateAsync(subTopic, cancellationToken);

        if (rowsAffected <= 0)
        {
            throw new Exception("Failed to update sub topic.");
        }

        return new UpdateSubTopicResponse
        {
            Success = true,
            Message = "Sub topic updated successfully."
        };
    }
}