using MediatR;
using TechLoop.Application.Features.Topics.DTOs;

namespace TechLoop.Application.Features.Topics.Queries.GetAllTopics;

public sealed record GetAllTopicQuery : IRequest<IEnumerable<TopicResponse>>;