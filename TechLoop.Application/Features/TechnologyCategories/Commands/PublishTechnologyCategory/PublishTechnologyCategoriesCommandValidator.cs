using FluentValidation;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.TechnologyCategories.Commands.PublishTechnologyCategory;

public sealed class PublishTechnologyCategoryCommandValidator : AbstractValidator<PublishTechnologyCategoryCommand>
{
    public PublishTechnologyCategoryCommandValidator(
        ITechnologyCategoryRepository repository)
    {
        RuleFor(x => x.Id)
            .MustAsync(repository.ExistsAsync)
            .WithMessage("Technology Category not found.");
    }
}