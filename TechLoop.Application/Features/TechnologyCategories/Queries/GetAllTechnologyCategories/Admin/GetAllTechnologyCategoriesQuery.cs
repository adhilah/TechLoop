using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;

namespace TechLoop.Application.Features.TechnologyCategories.Queries.GetAllTechnologyCategories.Admin;

public sealed record GetAllTechnologyCategoriesQuery : IRequest<IEnumerable<AdminTechnologyCategoryResponse>>;