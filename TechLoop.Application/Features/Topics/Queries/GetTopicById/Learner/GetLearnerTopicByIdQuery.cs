using MediatR;
using TechLoop.Application.Features.Topics.DTOs;

namespace TechLoop.Application.Features.Topics.Queries.GetTopicById.Learner;

public sealed record GetLearnerTopicByIdQuery(int Id ) : IRequest<LearnerTopicResponse>;