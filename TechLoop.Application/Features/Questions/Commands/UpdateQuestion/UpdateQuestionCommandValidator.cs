using FluentValidation;

namespace TechLoop.Application.Features.Questions.Commands.UpdateQuestion;

public sealed class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
{
    public UpdateQuestionCommandValidator()
    {
        RuleFor(x => x.SubTopicId)
            .GreaterThan(0)
            .NotEmpty()
            .WithMessage("SubTopicId cannot be empty");

        RuleFor(x => x.QuestionType)
            .IsInEnum();

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty")
            .MaximumLength(200)
            .WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithMessage("Slug cannot be empty")
            .MaximumLength(200)
            .WithMessage("Slug cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description cannot be empty");

        RuleFor(x => x.Mark)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Mark must be greater than or equal 0");

        RuleFor(x => x.TimeLimitSeconds)
            .GreaterThanOrEqualTo(0)
            .WithMessage("TimeLimitSeconds must be greater than or equal 0");

        RuleFor(x => x.MemoryLimitMb)
            .GreaterThanOrEqualTo(0)
            .WithMessage("MemoryLimitMb must be greater than or equal 0")
            .NotEmpty()
            .WithMessage("MemoryLimitMb cannot be empty");

        RuleFor(x => x.Position)
            .GreaterThan(0)
            .WithMessage("Position must be greater than or equal 0")
            .NotEmpty()
            .WithMessage("Position cannot be empty");

        RuleFor(x => x.Difficulty)
            .IsInEnum();
    }
}