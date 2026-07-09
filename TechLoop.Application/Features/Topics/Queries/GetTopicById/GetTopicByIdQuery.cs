using MediatR;
using TechLoop.Application.Features.Topics.DTOs;

namespace TechLoop.Application.Features.Topics.Queries.GetTopicById;

public sealed record GetTopicByIdQuery(
    int Id
) : IRequest<TopicResponse>;