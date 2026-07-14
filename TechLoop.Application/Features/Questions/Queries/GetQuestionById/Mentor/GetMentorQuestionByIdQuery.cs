using MediatR;
using TechLoop.Application.Features.Questions.DTOs;

namespace TechLoop.Application.Features.Questions.Queries.GetQuestionById.Mentor;

public sealed record GetMentorQuestionByIdQuery(int Id ) : IRequest<MentorQuestionResponse>;