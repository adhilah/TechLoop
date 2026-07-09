using MediatR;
using TechLoop.Application.Features.Topics.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;

namespace TechLoop.Application.Features.Topics.Commands.DeleteTopic;

public sealed class DeleteTopicCommandHandler : IRequestHandler<DeleteTopicCommand, DeleteTopicResponse>
{
    private readonly ITopicsRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public DeleteTopicCommandHandler(ITopicsRepository repository, ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<DeleteTopicResponse> Handle(
        DeleteTopicCommand request,
        CancellationToken cancellationToken)
    {
        var topic = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (topic is null)
        {
            throw new KeyNotFoundException("Topic not found.");
        }

        await _repository.SoftDeleteAsync(request.Id, _currentUser.UserId, cancellationToken);

        return new DeleteTopicResponse
        {
            Success = true,
            Message = "Topic deleted successfully."
        };
    }
}