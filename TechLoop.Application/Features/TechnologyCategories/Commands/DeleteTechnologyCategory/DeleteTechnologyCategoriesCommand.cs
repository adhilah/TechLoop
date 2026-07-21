using MediatR;

namespace TechLoop.Application.Features.TechnologyCategories.Commands.DeleteTechnologyCategories;

public sealed record DeleteTechnologyCategoryCommand (int Id, Guid DeletedBy ) : IRequest<Unit>;