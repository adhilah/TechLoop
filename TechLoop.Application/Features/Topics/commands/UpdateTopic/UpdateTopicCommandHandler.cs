using MediatR;
using TechLoop.Application.Features.Topics.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;

namespace TechLoop.Application.Features.Topics.Commands.UpdateTopic;

public sealed class UpdateTopicCommandHandler
    : IRequestHandler<UpdatedTopicCommand, UpdateTopicResponse>
{
    private readonly ITopicsRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public UpdateTopicCommandHandler(
        ITopicsRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<UpdateTopicResponse> Handle(
        UpdatedTopicCommand request,
        CancellationToken cancellationToken)
    {
        var topic = await _repository.GetByIdAsync(request.id, cancellationToken);
        if (topic is null)
        {
            throw new KeyNotFoundException("Topic not found.");
        }
        topic.TechnologyId = request.TechnologyId;
        topic.Title = request.Title;
        topic.Description = request.Description;
        topic.ImageUrl = request.ImageUrl;
        topic.Slug = request.Slug;
        topic.Position = request.Position;
        topic.Status = request.Status;

        topic.UpdatedBy = _currentUser.UserId;
        topic.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(topic, cancellationToken);

        return new UpdateTopicResponse
        {
            Success = true,
            Message = "Topic updated successfully."
        };
    }
}