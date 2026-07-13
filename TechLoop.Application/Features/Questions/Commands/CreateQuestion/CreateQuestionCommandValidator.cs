using FluentValidation;
using TechLoop.Domain.Enums;

namespace TechLoop.Application.Features.Questions.Commands.CreateQuestion;

public sealed class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator()
    {
        RuleFor(x => x.SubTopicId)
            .GreaterThan(0)
            .WithMessage("Sub topic is required.");

        RuleFor(x => x.QuestionType)
            .IsInEnum();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleFor(x => x.Mark)
            .GreaterThan(0)
            .WithMessage("Mark must be greater than 0.");

        RuleFor(x => x.Position)
            .GreaterThan(0);

        RuleFor(x => x.Difficulty)
            .IsInEnum();

        RuleFor(x => x.Status)
            .IsInEnum();
        
        When(x => x.QuestionType == QuestionType.coding, () =>
        {
            RuleFor(x => x.TimeLimitSeconds)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Time limit is required for coding questions.");

            RuleFor(x => x.MemoryLimitMb)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Memory limit is required for coding questions.");
        });
    }
}