using MediatR;
using TechLoop.Application.Features.Topics.DTOs;

namespace TechLoop.Application.Features.Topics.Commands.DeleteTopic;

public sealed record DeleteTopicCommand(int Id ) : IRequest<DeleteTopicResponse>;