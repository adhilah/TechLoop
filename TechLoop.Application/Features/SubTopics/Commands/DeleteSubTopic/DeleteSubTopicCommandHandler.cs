using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.SubTopics.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;

namespace TechLoop.Application.Features.SubTopics.Commands.DeleteSubTopic;

public sealed class DeleteSubTopicCommandHandler
    : IRequestHandler<DeleteSubTopicCommand, DeleteSubTopicResponse>
{
    private readonly ISubTopicsRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public DeleteSubTopicCommandHandler(
        ISubTopicsRepository repository,
        ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<DeleteSubTopicResponse> Handle(DeleteSubTopicCommand request, CancellationToken cancellationToken)
    {
        var subTopic = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (subTopic is null)
        {
            throw new NotFoundException("Sub topic not found.");
        }
        

        // Soft delete
        var rowsAffected = await _repository.SoftDeleteAsync(request.Id, _currentUserService.UserId, cancellationToken);
        if (rowsAffected <= 0)
        {
            throw new Exception("Failed to delete sub topic.");
        }

        return new DeleteSubTopicResponse
        {
            Success = true,
            Message = "Sub topic deleted successfully."
        };
    }
}