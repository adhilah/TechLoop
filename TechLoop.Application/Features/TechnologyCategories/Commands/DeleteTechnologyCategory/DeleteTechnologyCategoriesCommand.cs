using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;

namespace TechLoop.Application.Features.TechnologyCategories.Commands.DeleteTechnologyCategory;

public sealed record DeleteTechnologyCategoryCommand (int Id, Guid DeletedBy ) : IRequest<DeleteTechnologyCategoryResponse>;