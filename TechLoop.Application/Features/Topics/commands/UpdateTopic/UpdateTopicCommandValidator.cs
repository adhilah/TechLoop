using FluentValidation;

namespace TechLoop.Application.Features.Topics.Commands.UpdateTopic;

public sealed class UpdateTopicCommandValidator
    : AbstractValidator<UpdatedTopicCommand>
{
    public UpdateTopicCommandValidator()
    {
        RuleFor(x => x.id)
            .GreaterThan(0);

        RuleFor(x => x.TechnologyId)
            .GreaterThan(0);

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleFor(x => x.ImageUrl)
            .MaximumLength(255);

        RuleFor(x => x.Position)
            .GreaterThanOrEqualTo(0);
    }
}