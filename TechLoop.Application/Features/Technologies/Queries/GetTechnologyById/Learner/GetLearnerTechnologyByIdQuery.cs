using MediatR;
using TechLoop.Application.Features.Technologies.DTOs;

namespace TechLoop.Application.Features.Technologies.Queries.GetTechnologyById.Learner;

public sealed record GetLearnerTechnologyByIdQuery(int Id ) : IRequest<LearnerTechnologyResponse>;