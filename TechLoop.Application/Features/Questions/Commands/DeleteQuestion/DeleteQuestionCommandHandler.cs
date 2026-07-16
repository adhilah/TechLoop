using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.Questions.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;
using TechLoop.Domain.Enums;

namespace TechLoop.Application.Features.Questions.Commands.DeleteQuestion;

public sealed class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, DeleteQuestionResponse>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IMcqOptionRepository _mcqOptionRepository;
    private readonly ICurrentUserService _currentUserService;

    public DeleteQuestionCommandHandler(IQuestionRepository questionRepository, IMcqOptionRepository mcqOptionRepository, ICurrentUserService currentUserService)
    {
        _questionRepository = questionRepository;
        _mcqOptionRepository = mcqOptionRepository;
        _currentUserService = currentUserService;
    }

    public async Task<DeleteQuestionResponse> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        // Check whether the question exists
        var question = await _questionRepository.GetByIdAsync(request.Id, cancellationToken);
        if (question is null)
        {
            throw new NotFoundException("Question not found.");
        }

        // Cascade soft delete for MCQ options
        if (question.QuestionType == QuestionType.mcq)
        {
            await _mcqOptionRepository.SoftDeleteByQuestionIdAsync(question.Id, _currentUserService.UserId, cancellationToken);
        }

        // Soft delete question
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