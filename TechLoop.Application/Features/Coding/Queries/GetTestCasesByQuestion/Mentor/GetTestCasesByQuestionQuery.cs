using MediatR;
using TechLoop.Application.Features.Coding.DTOs;

namespace TechLoop.Application.Features.Coding.Queries.GetTestCasesByQuestion.Mentor;

public sealed record GetTestCasesByQuestionQuery(int QuestionId) : IRequest<IEnumerable<MentorTestCaseResponse>>;