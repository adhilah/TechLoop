using MediatR;
using TechLoop.Application.Features.SubTopics.DTOs;

namespace TechLoop.Application.Features.SubTopics.Queries.GetAllSubTopics;

public sealed record GetAllSubTopicsQuery : IRequest<SubTopicResponse>;