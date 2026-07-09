using MediatR;
using TechLoop.Application.Features.Topics.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;
using TechLoop.Domain.Entities;

namespace TechLoop.Application.Features.Topics.Commands.CreateTopic;

public sealed class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand, CreateTopicResponse>
{
    private readonly ITopicsRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public CreateTopicCommandHandler(ITopicsRepository repository, ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<CreateTopicResponse> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
    {
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

        return new CreateTopicResponse
        {
            Success = true,
            Message = "Topic created successfully."
        };
    }
}