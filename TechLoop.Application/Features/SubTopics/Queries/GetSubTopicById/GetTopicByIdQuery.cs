using MediatR;
using TechLoop.Application.Features.SubTopics.DTOs;

namespace TechLoop.Application.Features.SubTopics.Queries.GetSubTopicById;

public sealed record GetSubTopicByIdQuery(int Id) : IRequest<SubTopicResponse>;