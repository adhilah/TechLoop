using FluentValidation;

namespace TechLoop.Application.Features.MCQ.Commands.UpdateMcqOption;

public sealed class UpdateMcqOptionValidator : AbstractValidator<UpdateMcqOptionCommand>
{
    public UpdateMcqOptionValidator()
    {
        RuleFor(x => x.OptionText)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Position)
            .GreaterThan(0);
    }
}