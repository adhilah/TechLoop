using FluentValidation;

namespace TechLoop.Application.Features.SubTopics.Commands.PublishSubTopic;

public sealed class PublishSubTopicValidator : AbstractValidator<PublishSubTopicCommand>
{
    public PublishSubTopicValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}