using MediatR;
using TechLoop.Application.Features.SubTopics.DTOs;

namespace TechLoop.Application.Features.SubTopics.Queries.GetAllSubTopics.Learner;

public sealed record GetAllLearnerSubTopicsQuery : IRequest<IEnumerable<LearnerSubTopicResponse>>;