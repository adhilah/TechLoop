using FluentValidation;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.TechnologyCategories.Commands.UpdateTechnologyCategories;

public sealed class UpdateTechnologyCategoryCommandValidator
    : AbstractValidator<UpdateTechnologyCategoryCommand>
{
    public UpdateTechnologyCategoryCommandValidator(
        ITechnologyCategoryRepository repository)
    {
        RuleFor(x => x.Request.Id)
            .MustAsync(repository.ExistsAsync)
            .WithMessage("Technology Category not found.");

        RuleFor(x => x.Request.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}