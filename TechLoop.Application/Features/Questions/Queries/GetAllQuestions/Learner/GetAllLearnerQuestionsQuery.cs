using MediatR;
using TechLoop.Application.Features.Questions.DTOs;

namespace TechLoop.Application.Features.Questions.Queries.GetAllQuestions.Learner;

public sealed record GetAllLearnerQuestionsQuery : IRequest<IEnumerable<LearnerQuestionResponse>>;