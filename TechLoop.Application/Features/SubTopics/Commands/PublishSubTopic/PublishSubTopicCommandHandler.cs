using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.SubTopics.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;

namespace TechLoop.Application.Features.SubTopics.Commands.PublishSubTopic;

public sealed class PublishSubTopicCommandHandler : IRequestHandler<PublishSubTopicCommand, PublishSubTopicResponse>
{
    private readonly ISubTopicsRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public PublishSubTopicCommandHandler(ISubTopicsRepository repository, ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<PublishSubTopicResponse> Handle(PublishSubTopicCommand request, CancellationToken cancellationToken)
    {
        var subTopic = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (subTopic is null)
        {
            throw new NotFoundException("Sub topic not found.");
        }

        if (subTopic.PublishedAt is not null)
        {
            throw new ValidationException("Sub topic is already published.");
        }

        subTopic.PublishedAt = DateTime.UtcNow;
        subTopic.PublishedBy = _currentUser.UserId;


        var rowsAffected = await _repository.PublishAsync(subTopic, cancellationToken);

        if (rowsAffected <= 0)
        {
            throw new Exception("Failed to publish sub topic.");
        }

        return new PublishSubTopicResponse
        {
            Success = true,
            Message = "Sub topic published successfully."
        };
    }
}