using MediatR;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.TechnologyCategories.Commands.DeleteTechnologyCategories;

public sealed class DeleteTechnologyCategoryCommandHandler : IRequestHandler<DeleteTechnologyCategoryCommand, Unit>
{
    private readonly ITechnologyCategoryRepository _repository;
    public DeleteTechnologyCategoryCommandHandler(ITechnologyCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteTechnologyCategoryCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id, request.DeletedBy);
        return Unit.Value;
    }
}