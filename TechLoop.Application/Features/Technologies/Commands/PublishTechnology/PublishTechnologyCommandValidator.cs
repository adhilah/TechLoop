using FluentValidation;

namespace TechLoop.Application.Features.Technologies.Commands.PublishTechnology;

public sealed class PublishTechnologyCommandValidator : AbstractValidator<PublishTechnologyCommand>
{
    public PublishTechnologyCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}