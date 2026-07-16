using MediatR;
using TechLoop.Application.Features.Coding.DTOs;

namespace TechLoop.Application.Features.Coding.Queries.GetTestCasesByQuestion.Learner;

public sealed record GetPublishedTestCasesByQuestionQuery(int QuestionId) : IRequest<IEnumerable<LearnerTestCaseResponse>>;