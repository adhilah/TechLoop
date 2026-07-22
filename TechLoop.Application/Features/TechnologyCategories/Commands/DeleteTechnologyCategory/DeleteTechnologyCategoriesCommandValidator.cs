using FluentValidation;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.TechnologyCategories.Commands.DeleteTechnologyCategory;

public sealed class DeleteTechnologyCategoryCommandValidator : AbstractValidator<DeleteTechnologyCategoryCommand>
{
    public DeleteTechnologyCategoryCommandValidator(ITechnologyCategoryRepository repository)
    {
        RuleFor(x => x.Id)
            .MustAsync(repository.ExistsAsync)
            .WithMessage("Technology Category not found.");
    }
}