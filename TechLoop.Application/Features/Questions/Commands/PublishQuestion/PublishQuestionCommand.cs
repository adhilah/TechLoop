using MediatR;
using TechLoop.Application.Features.Questions.DTOs;

namespace TechLoop.Application.Features.Questions.Commands.PublishQuestion;

public sealed record PublishQuestionCommand(int Id ) : IRequest<PublishQuestionResponse>;