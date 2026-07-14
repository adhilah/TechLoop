using MediatR;
using TechLoop.Application.Features.SubTopics.DTOs;

namespace TechLoop.Application.Features.SubTopics.Commands.PublishSubTopic;

public sealed record PublishSubTopicCommand(int Id ) : IRequest<PublishSubTopicResponse>;