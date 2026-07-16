using MediatR;
using TechLoop.Application.Features.MCQ.DTOs;

namespace TechLoop.Application.Features.MCQ.Queries.GetMcqOptionsByQuestionQuery.Learner;

public sealed record GetPublishedMcqOptionByIdQuery(int QuestionId) : IRequest<IEnumerable<LearnerMcqOptionResponse>>;