using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.Coding.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;

namespace TechLoop.Application.Features.Coding.Commands.UpdateTestCase;

public sealed class UpdateTestCaseCommandHandler : IRequestHandler<UpdateTestCaseCommand, UpdateTestCaseResponse>
{
    private readonly ITestCaseRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public UpdateTestCaseCommandHandler(ITestCaseRepository repository, ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<UpdateTestCaseResponse> Handle(UpdateTestCaseCommand request, CancellationToken cancellationToken)
    {
        var testCase = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (testCase is null)
            throw new NotFoundException("Test case not found.");

        if (testCase.Position != request.Position)
        {
            var positionExists = await _repository.PositionExistsAsync(testCase.QuestionId, request.Position, cancellationToken);
            if (positionExists)
                throw new ValidationException($"Position '{request.Position}' already exists.");
        }

        testCase.Input = request.Input.Trim();
        testCase.ExpectedOutput = request.ExpectedOutput.Trim();
        testCase.IsHidden = request.IsHidden;
        testCase.Position = request.Position;
        testCase.UpdatedAt = DateTime.UtcNow;
        testCase.UpdatedBy = _currentUser.UserId;

        await _repository.UpdateAsync(testCase, cancellationToken);
        return new UpdateTestCaseResponse
        {
            Success = true,
            Message = "Test case updated successfully."
        };
    }
}