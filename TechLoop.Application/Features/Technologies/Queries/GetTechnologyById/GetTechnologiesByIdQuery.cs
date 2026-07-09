using MediatR;
using TechLoop.Application.Features.Technologies.DTOs;

namespace TechLoop.Application.Features.Technologies.Queries.GetTechnologyById;

public sealed record GetTechnologyByIdQuery(
    int Id
) : IRequest<TechnologyResponse>;