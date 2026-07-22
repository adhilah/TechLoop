using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;

namespace TechLoop.Application.Features.TechnologyCategories.Commands.CreateTechnologyCategory;

public sealed record CreateTechnologyCategoriesCommand
(
    CreateTechnologyCategoryRequest Request,
    Guid CreatedBy
) : IRequest<CreateTechnologyCategoryResponse>;