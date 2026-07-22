using FluentValidation;

namespace TechLoop.Application.Features.TechnologyCategories.Commands.CreateTechnologyCategory;

public sealed class CreateTechnologyCategoriesCommandValidator
    : AbstractValidator<CreateTechnologyCategoriesCommand>
{
    public CreateTechnologyCategoriesCommandValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(500)
            .WithMessage("Name must not exceed 500 characters");
        
    }
}