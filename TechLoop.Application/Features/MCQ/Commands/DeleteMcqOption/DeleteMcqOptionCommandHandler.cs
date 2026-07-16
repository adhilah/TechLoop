using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;

namespace TechLoop.Application.Features.MCQ.Commands.DeleteMcqOption;

public sealed class DeleteMcqOptionCommandHandler : IRequestHandler<DeleteMcqOptionCommand, bool>
{
    private readonly IMcqOptionRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public DeleteMcqOptionCommandHandler(IMcqOptionRepository repository, ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(DeleteMcqOptionCommand request, CancellationToken cancellationToken)
    {
        var option = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (option is null)
            throw new NotFoundException("MCQ option not found.");

        var affectedRows = await _repository.SoftDeleteAsync(request.Id, _currentUser.UserId, cancellationToken);
        if (affectedRows == 0)
            throw new ValidationException("Unable to delete MCQ option.");

        return true;
    }
}