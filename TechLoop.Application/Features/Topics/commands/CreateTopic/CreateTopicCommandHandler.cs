using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.Topics.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;
using TechLoop.Domain.Entities;

namespace TechLoop.Application.Features.Topics.Commands.CreateTopic;

public sealed class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand, CreateTopicResponse>
{
    private readonly ITopicsRepository _repository;
    private readonly ITechnologyRepository _technologyRepository;   
    private readonly ICurrentUserService _currentUser;

    public CreateTopicCommandHandler(ITopicsRepository repository,ITechnologyRepository technologyRepository, ICurrentUserService currentUser)
    {
        _repository = repository;
        _technologyRepository = technologyRepository;
        _currentUser = currentUser;
    }

    public async Task<CreateTopicResponse> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
    {
        var technology = await _repository.GetByIdAsync(request.TechnologyId, cancellationToken);

        if (technology is null)
        {
            throw new NotFoundException("Technology not found.");
        }
        var exists = await _repository.ExistsAsync(request.Title,cancellationToken);

        if (exists)
        {
            throw new ValidationException($"Topic '{request.Title}' already exists.");
        }
        var topic = new Topic
        {
            TechnologyId = request.TechnologyId,
            Title = request.Title,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            Slug = request.Slug,
            Position = request.Position,
            Status = request.Status,
            CreatedBy = _currentUser.UserId,
            CreatedAt = DateTime.UtcNow
        };

        var id = await _repository.CreateAsync(topic, cancellationToken);
        
        var createdTopic = await _repository.GetByIdAsync(id, cancellationToken);
        if (createdTopic is null)
        {
            throw new Exception("Failed to create topic.");
        }

        return new CreateTopicResponse
        {
            Success = true,
            Message = "Topic created successfully."
        };
    }
}