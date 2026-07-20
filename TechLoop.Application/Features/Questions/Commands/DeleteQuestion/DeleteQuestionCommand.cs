using MediatR;
using TechLoop.Application.Features.Questions.DTOs;

namespace TechLoop.Application.Features.Questions.Commands.DeleteQuestion;

public sealed record DeleteQuestionCommand(int Id) : IRequest<DeleteQuestionResponse>;