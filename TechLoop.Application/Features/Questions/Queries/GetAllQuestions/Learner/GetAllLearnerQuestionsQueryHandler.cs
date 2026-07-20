using MediatR;
using TechLoop.Application.Features.Questions.DTOs;
using TechLoop.Application.Features.Questions.Queries.GetAllQuestions.Learner;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.Questions.Queries.GetAllLearnerQuestions;

public sealed class GetAllLearnerQuestionsQueryHandler : IRequestHandler<GetAllLearnerQuestionsQuery, IEnumerable<LearnerQuestionResponse>>
{
    private readonly IQuestionRepository _repository;
    public GetAllLearnerQuestionsQueryHandler(IQuestionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LearnerQuestionResponse>> Handle(GetAllLearnerQuestionsQuery request, CancellationToken cancellationToken)
    {
        var questions = await _repository.GetPublishedAsync(cancellationToken);
        return questions.Select(question => new LearnerQuestionResponse
        {
            Id = question.Id,
            SubTopicId = question.SubTopicId,
            QuestionType = question.QuestionType,
            Slug = question.Slug,
            Title = question.Title,
            Description = question.Description,
            ImageUrl = question.ImageUrl,
            Mark = question.Mark,
            Hint = question.Hint,
            TimeLimitSeconds = question.TimeLimitSeconds,
            MemoryLimitMb = question.MemoryLimitMb,
            Difficulty = question.Difficulty
        });
    }
}