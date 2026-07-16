using MediatR;
using TechLoop.Application.Features.Coding.DTOs;

namespace TechLoop.Application.Features.Coding.Queries.GetCodingTemplatesByQuestion.Mentor;

public sealed record GetCodingTemplatesByQuestionQuery(int QuestionId) : IRequest<IEnumerable<MentorCodingTemplateResponse>>;