using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.MCQ.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;
using TechLoop.Domain.Entities;
using TechLoop.Domain.Enums;

namespace TechLoop.Application.Features.MCQ.Commands.CreateMcqOption;

public sealed class CreateMcqOptionCommandHandler : IRequestHandler<CreateMcqOptionCommand, CreateMcqOptionResponse>
{
    private readonly IMcqOptionRepository _repository;
    private readonly IQuestionRepository _questionRepository;
    private readonly ICurrentUserService _currentUser;

    public CreateMcqOptionCommandHandler(IMcqOptionRepository repository, IQuestionRepository questionRepository, ICurrentUserService currentUser)
    {
        _repository = repository;
        _questionRepository = questionRepository;
        _currentUser = currentUser;
    }

    public async Task<CreateMcqOptionResponse> Handle(CreateMcqOptionCommand request, CancellationToken cancellationToken)
    {
        // Check whether the question exists
        var question = await _questionRepository.GetByIdAsync(request.QuestionId, cancellationToken);
        if (question is null)
        {
            throw new NotFoundException("Question not found.");
        }

        // Only MCQ questions can have MCQ options
        if (question.QuestionType != QuestionType.mcq)
        {
            throw new ValidationException("MCQ options can only be added to MCQ questions.");
        }

        var optionText = request.OptionText.Trim();

        // Check duplicate option text
        var optionExists = await _repository.ExistsAsync(request.QuestionId, optionText, cancellationToken);
        if (optionExists)
        {
            throw new ValidationException($"Option '{optionText}' already exists.");
        }
        
        //check position count
        var optionCount = await _repository.GetOptionCountAsync(request.QuestionId, cancellationToken);
        if (optionCount >= 4)
        {
            throw new ValidationException(
                "An MCQ question can have a maximum of 4 options.");
        }

        // Check duplicate position
        var positionExists = await _repository.PositionExistsAsync(request.QuestionId, request.Position, cancellationToken);
        if (positionExists)
        {
            throw new ValidationException($"Position '{request.Position}' already exists.");
        }

        // Allow only one correct answer (optional business rule)
        if (request.IsCorrect)
        {
            var hasCorrectOption = await _repository.HasCorrectOptionAsync(request.QuestionId, cancellationToken);
            if (hasCorrectOption)
            {
                throw new ValidationException("This question already has a correct option.");
            }
        }

        var option = new McqOption
        {
            QuestionId = request.QuestionId,
            OptionText = optionText,
            IsCorrect = request.IsCorrect,
            Position = request.Position,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = _currentUser.UserId
        };

        await _repository.CreateAsync(option, cancellationToken);
        return new CreateMcqOptionResponse
        {
            Success = true,
            Message = "MCQ option created successfully."
        };
    }
}