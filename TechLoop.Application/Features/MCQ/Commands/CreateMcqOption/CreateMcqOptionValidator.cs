using FluentValidation;

namespace TechLoop.Application.Features.MCQ.Commands.CreateMcqOption;

public sealed class CreateMcqOptionValidator : AbstractValidator<CreateMcqOptionCommand>
{
    public CreateMcqOptionValidator()
    {
        RuleFor(x => x.QuestionId)
            .GreaterThan(0);

        RuleFor(x => x.OptionText)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Position)
            .GreaterThan(0);
    }
}