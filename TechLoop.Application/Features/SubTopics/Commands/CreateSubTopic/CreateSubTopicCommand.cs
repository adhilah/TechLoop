using MediatR;
using TechLoop.Application.Features.SubTopics.DTOs;
using TechLoop.Domain.Enums;

namespace TechLoop.Application.Features.SubTopics.Commands.CreateSubTopic;

public sealed record CreateSubTopicCommand(
    int TopicId,
    string Title,
    string Description,
    string? ImageUrl,
    string Slug,
    int Position
) : IRequest<CreateSubTopicResponse>;