using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.Coding.DTOs;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.Coding.Queries.GetCodingTemplatesByQuestion.Learner;

public sealed class GetPublishedCodingTemplatesByQuestionQueryHandler : IRequestHandler<GetPublishedCodingTemplatesByQuestionQuery, IEnumerable<LearnerCodingTemplateResponse>>
{
    private readonly ICodingTemplateRepository _codingTemplateRepository;
    private readonly IQuestionRepository _questionRepository;

    public GetPublishedCodingTemplatesByQuestionQueryHandler(ICodingTemplateRepository codingTemplateRepository, IQuestionRepository questionRepository)
    {
        _codingTemplateRepository = codingTemplateRepository;
        _questionRepository = questionRepository;
    }

    public async Task<IEnumerable<LearnerCodingTemplateResponse>> Handle(GetPublishedCodingTemplatesByQuestionQuery request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetPublishedByIdAsync(request.QuestionId, cancellationToken);
        if (question is null)
            throw new NotFoundException("Published question not found.");

        var templates = await _codingTemplateRepository.GetByQuestionIdAsync(request.QuestionId, cancellationToken);
        return templates.Select(x => new LearnerCodingTemplateResponse
        {
            Id = x.Id,
            TechnologyId = x.TechnologyId,
            StarterCode = x.StarterCode
        });
    }
}