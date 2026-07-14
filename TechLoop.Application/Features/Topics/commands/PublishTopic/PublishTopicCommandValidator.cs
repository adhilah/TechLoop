using FluentValidation;

namespace TechLoop.Application.Features.Topics.Commands.PublishTopic;

public sealed class PublishTopicValidator : AbstractValidator<PublishTopicCommand>
{
    public PublishTopicValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}