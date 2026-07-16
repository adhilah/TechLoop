using FluentValidation;

namespace TechLoop.Application.Features.Coding.Commands.DeleteCodingTemplate;

public sealed class DeleteCodingTemplateCommandValidator : AbstractValidator<DeleteCodingTemplateCommand>
{
    public DeleteCodingTemplateCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Coding template id is required.");
    }
}