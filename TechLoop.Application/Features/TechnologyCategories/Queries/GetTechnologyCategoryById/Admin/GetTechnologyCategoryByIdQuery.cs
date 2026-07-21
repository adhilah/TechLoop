using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;

namespace TechLoop.Application.Features.TechnologyCategories.Queries.GetTechnologyCategoriesById.Admin;

public sealed record GetTechnologyCategoriesByIdQuery(int Id) : IRequest<AdminTechnologyCategoryResponse?>;