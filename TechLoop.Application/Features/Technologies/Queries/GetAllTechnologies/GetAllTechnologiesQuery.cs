using MediatR;
using TechLoop.Application.Features.Technologies.DTOs;

namespace TechLoop.Application.Features.Technologies.Queries.GetAllTechnologies;

public sealed record GetAllTechnologiesQuery()
    : IRequest<List<TechnologyResponse>>;