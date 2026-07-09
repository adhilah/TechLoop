using FluentValidation;

namespace TechLoop.Application.Features.Topics.Commands.DeleteTopic;

public sealed class DeleteTopicCommandValidator : AbstractValidator<DeleteTopicCommand>
{
    public DeleteTopicCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}