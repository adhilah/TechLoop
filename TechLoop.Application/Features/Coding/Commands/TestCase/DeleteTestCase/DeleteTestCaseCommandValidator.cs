using FluentValidation;

namespace TechLoop.Application.Features.Coding.Commands.DeleteTestCase;

public sealed class DeleteTestCaseCommandValidator : AbstractValidator<DeleteTestCaseCommand>
{
    public DeleteTestCaseCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Test case id is required.");
    }
}