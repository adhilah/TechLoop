using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.Topics.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;

namespace TechLoop.Application.Features.Topics.Commands.UpdateTopic;

public sealed class UpdateTopicCommandHandler
    : IRequestHandler<UpdatedTopicCommand, UpdateTopicResponse>
{
    private readonly ITopicsRepository _repository;
    private readonly ITechnologyRepository _technologyRepository;
    private readonly ICurrentUserService _currentUser;

    public UpdateTopicCommandHandler(
        ITopicsRepository repository,ITechnologyRepository technologyRepository,ICurrentUserService currentUser)
    {
        _repository = repository;
        _technologyRepository = technologyRepository;
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
        
        var technology = await _technologyRepository.GetByIdAsync(
            request.TechnologyId,
            cancellationToken);

        if (technology is null)
        {
            throw new NotFoundException("Technology not found.");
        }

        // Check duplicate topic
        var exists = await _repository.ExistsAsync(
            request.Title,
            cancellationToken);

        if (exists &&
            !topic.Title.Equals(request.Title, StringComparison.OrdinalIgnoreCase))
        {
            throw new ValidationException(
                $"Topic '{request.Title}' already exists.");
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