using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;

namespace TechLoop.Application.Features.TechnologyCategories.Commands.UpdateTechnologyCategory;

public sealed record UpdateTechnologyCategoryCommand
(
    int Id,
    UpdateTechnologyCategoryRequest Request,
    Guid UpdatedBy
) : IRequest<UpdateTechnologyCategoryResponse>;