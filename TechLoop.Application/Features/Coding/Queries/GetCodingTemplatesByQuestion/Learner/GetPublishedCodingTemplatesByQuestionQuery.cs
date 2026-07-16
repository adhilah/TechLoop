using MediatR;
using TechLoop.Application.Features.Coding.DTOs;

namespace TechLoop.Application.Features.Coding.Queries.GetCodingTemplatesByQuestion.Learner;

public sealed record GetPublishedCodingTemplatesByQuestionQuery(int QuestionId) : IRequest<IEnumerable<LearnerCodingTemplateResponse>>;