using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;

namespace TechLoop.Application.Features.TechnologyCategories.Commands.UpdateTechnologyCategories;

public sealed record UpdateTechnologyCategoryCommand
(
    UpdateTechnologyCategoryRequest Request,
    Guid UpdatedBy
) : IRequest<UpdateTechnologyCategoryResponse>;