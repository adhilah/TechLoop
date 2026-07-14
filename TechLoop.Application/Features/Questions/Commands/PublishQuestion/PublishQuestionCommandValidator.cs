using FluentValidation;

namespace TechLoop.Application.Features.Questions.Commands.PublishQuestion;

public sealed class PublishQuestionValidator : AbstractValidator<PublishQuestionCommand>
{
    public PublishQuestionValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}