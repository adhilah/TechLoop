using MediatR;
using TechLoop.Application.Features.Questions.DTOs;

namespace TechLoop.Application.Features.Questions.Queries.GetAllMentorQuestions;

public sealed record GetAllMentorQuestionsQuery : IRequest<IEnumerable<MentorQuestionResponse>>;