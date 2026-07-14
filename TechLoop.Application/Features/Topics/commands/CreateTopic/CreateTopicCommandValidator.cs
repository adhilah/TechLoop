using FluentValidation;
using TechLoop.Domain.Enums;

namespace TechLoop.Application.Features.Topics.Commands.CreateTopic;

public sealed class CreateTopicCommandValidator
    : AbstractValidator<CreateTopicCommand>
{
    public CreateTopicCommandValidator()
    {
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