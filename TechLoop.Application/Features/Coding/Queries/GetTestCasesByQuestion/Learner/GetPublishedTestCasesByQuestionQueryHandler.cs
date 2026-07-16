using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.Coding.DTOs;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.Coding.Queries.GetTestCasesByQuestion.Learner;

public sealed class GetPublishedTestCasesByQuestionQueryHandler : IRequestHandler<GetPublishedTestCasesByQuestionQuery, IEnumerable<LearnerTestCaseResponse>>
{
    private readonly ITestCaseRepository _repository;
    private readonly IQuestionRepository _questionRepository;

    public GetPublishedTestCasesByQuestionQueryHandler(ITestCaseRepository repository, IQuestionRepository questionRepository)
    {
        _repository = repository;
        _questionRepository = questionRepository;
    }

    public async Task<IEnumerable<LearnerTestCaseResponse>> Handle(GetPublishedTestCasesByQuestionQuery request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetPublishedByIdAsync(request.QuestionId, cancellationToken);
        if (question is null)
            throw new NotFoundException("Published question not found.");

        var testCases = await _repository.GetVisibleByQuestionIdAsync(request.QuestionId, cancellationToken);
        return testCases.Select(x => new LearnerTestCaseResponse
        {
            Id = x.Id,
            Input = x.Input,
            ExpectedOutput = x.ExpectedOutput,
            Position = x.Position
        });
    }
}