using MediatR;
using TechLoop.Application.Features.SubTopics.DTOs;

namespace TechLoop.Application.Features.SubTopics.Commands.DeleteSubTopic;

public sealed record DeleteSubTopicCommand(int Id)
    : IRequest<DeleteSubTopicResponse>;