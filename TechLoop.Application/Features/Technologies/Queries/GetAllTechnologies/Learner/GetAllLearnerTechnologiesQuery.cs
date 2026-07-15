using MediatR;
using TechLoop.Application.Features.Technologies.DTOs;

namespace TechLoop.Application.Features.Technologies.Queries.GetAllTechnologies.Learner;

public sealed record GetAllLearnerTechnologiesQuery : IRequest<IEnumerable<LearnerTechnologyResponse>>;