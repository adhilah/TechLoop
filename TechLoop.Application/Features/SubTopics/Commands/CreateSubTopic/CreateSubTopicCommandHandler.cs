using FluentValidation;
using MediatR;
using TechLoop.Application.Features.SubTopics.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;
using TechLoop.Domain.Entities;
using TechLoop.Domain.Enums;

namespace TechLoop.Application.Features.SubTopics.Commands.CreateSubTopic;

public sealed class CreateSubTopicCommandHandler : IRequestHandler<CreateSubTopicCommand, CreateSubTopicResponse>
{
    private readonly ISubTopicsRepository _subTopicsRepository;
    private readonly ITopicsRepository _topicsRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateSubTopicCommandHandler(ISubTopicsRepository subTopicsRepository, ITopicsRepository topicsRepository, ICurrentUserService currentUserService)
    {
        _subTopicsRepository = subTopicsRepository;
        _topicsRepository = topicsRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CreateSubTopicResponse> Handle(CreateSubTopicCommand request, CancellationToken cancellationToken)
    {
        var topic = await _topicsRepository.GetByIdAsync(request.TopicId, cancellationToken);
        if (topic is null)
        {
            throw new InvalidOperationException("Topic not found.");
        }
            
        var slugExists = await _topicsRepository.SlugExistsAsync(request.Slug, cancellationToken);
        if (slugExists)
        {
            throw new ValidationException($"Technology slug '{request.Slug}' already exists.");
        }
        var positionExists = await _topicsRepository.PositionExistsAsync(request.Position, cancellationToken);
        if (positionExists)
        {
            throw new ValidationException($"Technology position '{request.Position}' already exists.");
        }
        var exists = await _subTopicsRepository.ExistsAsync(request.Slug, cancellationToken);

        if (exists)
        {
            throw new InvalidOperationException($"Sub topic with slug '{request.Slug}' already exists.");
        }

        var subTopic = new SubTopic
        {
            TopicId = request.TopicId,
            Title = request.Title.Trim(),
            Slug = request.Slug.Trim().ToLowerInvariant(),
            Description = request.Description ?? string.Empty,
            ImageUrl = request.ImageUrl ?? string.Empty,
            Position = request.Position,
            Status = request.Status,
            PublishedAt = request.Status == ContentStatus.Published ? DateTime.UtcNow : null,
            PublishedBy = request.Status == ContentStatus.Published ? _currentUserService.UserId : null,
            CreatedBy = _currentUserService.UserId,
            CreatedAt = DateTime.UtcNow,

        };

        var id = await _subTopicsRepository.CreateAsync(subTopic, cancellationToken);
        var createdSubTopic = await _subTopicsRepository.GetByIdAsync(id, cancellationToken);
        if (createdSubTopic is null)
        {
            throw new Exception("Failed to create sub topic.");
        }

        return new CreateSubTopicResponse
        {
            Success = true,
            Message = "Sub topic created successfully."
        };
    }

    private static string GenerateSlug(string value)
    {
        return value
            .Trim()
            .ToLowerInvariant()
            .Replace(" ", "-")
            .Replace("--", "-");
    }
}