using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.Coding.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;

namespace TechLoop.Application.Features.Coding.Commands.DeleteCodingTemplate;

public sealed class DeleteCodingTemplateCommandHandler : IRequestHandler<DeleteCodingTemplateCommand, DeleteCodingTemplateResponse>
{
    private readonly ICodingTemplateRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public DeleteCodingTemplateCommandHandler(ICodingTemplateRepository repository, ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<DeleteCodingTemplateResponse> Handle(DeleteCodingTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (template is null)
            throw new NotFoundException("Coding template not found.");

        var rowsAffected = await _repository.SoftDeleteAsync(request.Id, _currentUser.UserId, cancellationToken);
        if (rowsAffected <= 0)
            throw new Exception("Failed to delete coding template.");

        return new DeleteCodingTemplateResponse
        {
            Success = true,
            Message = "Coding template deleted successfully."
        };
    }
}