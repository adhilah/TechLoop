using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.MCQ.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;

namespace TechLoop.Application.Features.MCQ.Commands.UpdateMcqOption;

public sealed class UpdateMcqOptionCommandHandler : IRequestHandler<UpdateMcqOptionCommand, UpdateMcqOptionResponse>
{
    private readonly IMcqOptionRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public UpdateMcqOptionCommandHandler(IMcqOptionRepository repository, ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<UpdateMcqOptionResponse> Handle(UpdateMcqOptionCommand request, CancellationToken cancellationToken)
    {
        var option = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (option is null)
            throw new NotFoundException("MCQ option not found.");

        var optionText = request.OptionText.Trim();
        if (!option.OptionText.Equals(optionText, StringComparison.OrdinalIgnoreCase))
        {
            var exists = await _repository.ExistsAsync(option.QuestionId, optionText, cancellationToken);
            if (exists)
                throw new ValidationException($"Option '{optionText}' already exists.");
        }

        //
        
        if (option.Position != request.Position)
        {
            var positionExists = await _repository.PositionExistsAsync(option.QuestionId, request.Position, cancellationToken);
            if (positionExists)
                throw new ValidationException($"Position '{request.Position}' already exists.");
        }

        if (request.IsCorrect && !option.IsCorrect)
        {
            var hasCorrect = await _repository.HasCorrectOptionAsync(option.QuestionId, cancellationToken);
            if (hasCorrect)
                throw new ValidationException("This question already has a correct option.");
        }

        option.OptionText = optionText;
        option.IsCorrect = request.IsCorrect;
        option.Position = request.Position;
        option.UpdatedAt = DateTime.UtcNow;
        option.UpdatedBy = _currentUser.UserId;

        await _repository.UpdateAsync(option, cancellationToken);
        return new UpdateMcqOptionResponse
        {
            Success = true,
            Message = "MCQ option updated successfully."
        };
    }
}