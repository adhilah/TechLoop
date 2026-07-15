using MediatR;
using TechLoop.Application.Features.Topics.DTOs;

namespace TechLoop.Application.Features.Topics.Queries.GetAllTopics.Learner;

public sealed record GetAllLearnerTopicsQuery : IRequest<IEnumerable<LearnerTopicResponse>>;