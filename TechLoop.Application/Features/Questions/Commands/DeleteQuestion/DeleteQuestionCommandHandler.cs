using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.Questions.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;

namespace TechLoop.Application.Features.Questions.Commands.DeleteQuestion;

public sealed class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, DeleteQuestionResponse>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly ICurrentUserService _currentUserService;

    public DeleteQuestionCommandHandler(IQuestionRepository questionRepository, ICurrentUserService currentUserService)
    {
        _questionRepository = questionRepository;
        _currentUserService = currentUserService;
    }

    public async Task<DeleteQuestionResponse> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetByIdAsync(request.Id, cancellationToken);
        if (question is null)
        {
            throw new NotFoundException("Question not found.");
        }

        var rowsAffected = await _questionRepository.SoftDeleteAsync(request.Id, _currentUserService.UserId, cancellationToken);
        if (rowsAffected <= 0)
        {
            throw new Exception("Failed to delete question.");
        }

        return new DeleteQuestionResponse
        {
            Success = true,
            Message = "Question deleted successfully."
        };
    }
}