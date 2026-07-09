using TechLoop.Application.Features.Topics.DTOs;
using MediatR;

namespace TechLoop.Application.Features.Topics.Commands.UpdateTopic;

public sealed record UpdatedTopicCommand(
    int id,
    int TechnologyId,
    string Title,
    string Description,
    string ImageUrl,
    string Slug,
    int Position,
    string Status
    ):IRequest<UpdateTopicResponse>;

