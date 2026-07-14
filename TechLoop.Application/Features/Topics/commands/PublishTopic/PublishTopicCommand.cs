using MediatR;
using TechLoop.Application.Features.Topics.DTOs;

namespace TechLoop.Application.Features.Topics.Commands.PublishTopic;

public sealed record PublishTopicCommand(
    int Id
) : IRequest<PublishTopicResponse>;