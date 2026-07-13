using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.Questions.DTOs;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.Questions.Queries.GetMentorQuestionById;

public sealed class GetMentorQuestionByIdQueryHandler : IRequestHandler<GetMentorQuestionByIdQuery, MentorQuestionResponse>
{
    private readonly IQuestionRepository _repository;

    public GetMentorQuestionByIdQueryHandler(IQuestionRepository repository)
    {
        _repository = repository;
    }

    public async Task<MentorQuestionResponse> Handle(GetMentorQuestionByIdQuery request, CancellationToken cancellationToken)
    {
        var question = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (question is null)
        {
            throw new NotFoundException("Question not found.");
        }

        return new MentorQuestionResponse()
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
            Status = question.Status,
            PublishedAt = question.PublishedAt,
            PublishedBy = question.PublishedBy,
            CreatedAt = question.CreatedAt,
            CreatedBy = question.CreatedBy,
            UpdatedAt = question.UpdatedAt,
            UpdatedBy = question.UpdatedBy,
            DeletedAt = question.DeletedAt,
            DeletedBy = question.DeletedBy
        };
    }
}