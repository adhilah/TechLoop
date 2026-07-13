using MediatR;
using TechLoop.Application.Features.Questions.DTOs;

namespace TechLoop.Application.Features.Questions.Queries.GetLearnerQuestionById;

public sealed record GetLearnerQuestionByIdQuery(int Id) : IRequest<LearnerQuestionResponse>;