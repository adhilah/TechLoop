using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.MCQ.DTOs;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.MCQ.Queries.GetMcqOptionsByQuestionQuery.Learner;

public sealed class GetPublishedMcqOptionByIdQueryHandler : IRequestHandler<GetPublishedMcqOptionByIdQuery, IEnumerable<LearnerMcqOptionResponse>>
{
    private readonly IMcqOptionRepository _mcqOptionRepository;
    private readonly IQuestionRepository _questionRepository;

    public GetPublishedMcqOptionByIdQueryHandler(IMcqOptionRepository mcqOptionRepository, IQuestionRepository questionRepository)
    {
        _mcqOptionRepository = mcqOptionRepository;
        _questionRepository = questionRepository;
    }

    public async Task<IEnumerable<LearnerMcqOptionResponse>> Handle(GetPublishedMcqOptionByIdQuery request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetPublishedByIdAsync(request.QuestionId, cancellationToken);
        if (question is null)
            throw new NotFoundException("Published question not found.");

        var options = await _mcqOptionRepository.GetByQuestionIdAsync(request.QuestionId, cancellationToken);
        if (!options.Any())
        {
            throw new NotFoundException("No MCQ options found.");
        }
        return options.Select(x => new LearnerMcqOptionResponse
        {
            Id = x.Id,
            OptionText = x.OptionText,
            Position = x.Position
        });
    }
}