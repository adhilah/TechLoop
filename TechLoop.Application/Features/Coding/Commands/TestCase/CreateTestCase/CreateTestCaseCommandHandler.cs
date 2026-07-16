using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.Coding.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;
using TechLoop.Domain.Entities;
using TechLoop.Domain.Enums;

namespace TechLoop.Application.Features.Coding.Commands.CreateTestCase;

public sealed class CreateTestCaseCommandHandler : IRequestHandler<CreateTestCaseCommand, CreateTestCaseResponse>
{
    private readonly ITestCaseRepository _testCaseRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly ICurrentUserService _currentUser;

    public CreateTestCaseCommandHandler(ITestCaseRepository testCaseRepository, IQuestionRepository questionRepository, ICurrentUserService currentUser)
    {
        _testCaseRepository = testCaseRepository;
        _questionRepository = questionRepository;
        _currentUser = currentUser;
    }

    public async Task<CreateTestCaseResponse> Handle(CreateTestCaseCommand request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetByIdAsync(request.QuestionId, cancellationToken);
        if (question is null)
            throw new NotFoundException("Question not found.");

        if (question.QuestionType != QuestionType.coding)
            throw new ValidationException("Test cases can only be added to coding questions.");

        var positionExists = await _testCaseRepository.PositionExistsAsync(request.QuestionId, request.Position, cancellationToken);
        if (positionExists)
            throw new ValidationException($"Position '{request.Position}' already exists.");

        var testCase = new TestCase
        {
            QuestionId = request.QuestionId,
            Input = request.Input.Trim(),
            ExpectedOutput = request.ExpectedOutput.Trim(),
            IsHidden = request.IsHidden,
            Position = request.Position,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = _currentUser.UserId
        };

        await _testCaseRepository.CreateAsync(testCase, cancellationToken);
        return new CreateTestCaseResponse
        {
            Success = true,
            Message = "Test case created successfully."
        };
    }
}