using FluentValidation;

namespace TechLoop.Application.Features.Coding.Commands.UpdateTestCase;

public sealed class UpdateTestCaseCommandValidator : AbstractValidator<UpdateTestCaseCommand>
{
    public UpdateTestCaseCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Test case id is required.");

        RuleFor(x => x.Input)
            .NotEmpty()
            .WithMessage("Input is required.");

        RuleFor(x => x.ExpectedOutput)
            .NotEmpty()
            .WithMessage("Expected output is required.");

        RuleFor(x => x.Position)
            .GreaterThan(0)
            .WithMessage("Position must be greater than zero.");
    }
}