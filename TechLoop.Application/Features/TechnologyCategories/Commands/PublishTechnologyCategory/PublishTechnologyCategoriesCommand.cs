using MediatR;

namespace TechLoop.Application.Features.TechnologyCategories.Commands.PublishTechnologyCategories;

public sealed record PublishTechnologyCategoryCommand (int Id ) : IRequest<Unit>;