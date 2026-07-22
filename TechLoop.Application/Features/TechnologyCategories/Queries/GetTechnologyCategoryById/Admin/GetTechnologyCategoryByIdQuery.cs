using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;

namespace TechLoop.Application.Features.TechnologyCategories.Queries.GetTechnologyCategoryById.Admin;

public sealed record GetTechnologyCategoryByIdQuery(int Id) : IRequest<AdminTechnologyCategoryResponse?>;