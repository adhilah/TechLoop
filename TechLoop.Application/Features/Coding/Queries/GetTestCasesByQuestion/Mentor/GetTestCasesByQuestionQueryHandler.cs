using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.Coding.DTOs;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.Coding.Queries.GetTestCasesByQuestion.Mentor;

public sealed class GetTestCasesByQuestionQueryHandler : IRequestHandler<GetTestCasesByQuestionQuery, IEnumerable<MentorTestCaseResponse>>
{
    private readonly ITestCaseRepository _repository;
    private readonly IQuestionRepository _questionRepository;

    public GetTestCasesByQuestionQueryHandler(ITestCaseRepository repository, IQuestionRepository questionRepository)
    {
        _repository = repository;
        _questionRepository = questionRepository;
    }

    public async Task<IEnumerable<MentorTestCaseResponse>> Handle(GetTestCasesByQuestionQuery request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetByIdAsync(request.QuestionId, cancellationToken);
        if (question is null)
            throw new NotFoundException("Question not found.");

        var testCases = await _repository.GetByQuestionIdAsync(request.QuestionId, cancellationToken);
        return testCases.Select(x => new MentorTestCaseResponse
        {
            Id = x.Id,
            QuestionId = x.QuestionId,
            Input = x.Input,
            ExpectedOutput = x.ExpectedOutput,
            IsHidden = x.IsHidden,
            Position = x.Position,
            CreatedBy = x.CreatedBy,
            CreatedAt = x.CreatedAt,
            UpdatedBy = x.UpdatedBy,
            UpdatedAt = x.UpdatedAt
        });
    }
}