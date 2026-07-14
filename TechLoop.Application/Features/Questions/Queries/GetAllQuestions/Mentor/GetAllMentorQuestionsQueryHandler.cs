using MediatR;
using TechLoop.Application.Features.Questions.DTOs;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.Questions.Queries.GetAllQuestions.Mentor;

public sealed class GetAllMentorQuestionsQueryHandler
    : IRequestHandler<GetAllMentorQuestionsQuery, IEnumerable<MentorQuestionResponse>>
{
    private readonly IQuestionRepository _repository;

    public GetAllMentorQuestionsQueryHandler(
        IQuestionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MentorQuestionResponse>> Handle(
        GetAllMentorQuestionsQuery request,
        CancellationToken cancellationToken)
    {
        var questions = await _repository.GetAllAsync(cancellationToken);

        return questions.Select(question => new MentorQuestionResponse
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
            Explanation = question.Explanation,
            TimeLimitSeconds = question.TimeLimitSeconds,
            MemoryLimitMb = question.MemoryLimitMb,
            Difficulty = question.Difficulty,
            Position = question.Position,
            PublishedAt = question.PublishedAt,
            PublishedBy = question.PublishedBy,
            CreatedAt = question.CreatedAt,
            CreatedBy = question.CreatedBy,
            UpdatedAt = question.UpdatedAt,
            UpdatedBy = question.UpdatedBy,
            DeletedAt = question.DeletedAt,
            DeletedBy = question.DeletedBy
        });
    }
}