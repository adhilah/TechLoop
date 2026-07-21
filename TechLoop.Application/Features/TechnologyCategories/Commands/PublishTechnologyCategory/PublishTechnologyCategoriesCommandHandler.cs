using MediatR;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.TechnologyCategories.Commands.PublishTechnologyCategories;

public sealed class PublishTechnologyCategoryCommandHandler : IRequestHandler<PublishTechnologyCategoryCommand, Unit>
{
    private readonly ITechnologyCategoryRepository _repository;

    public PublishTechnologyCategoryCommandHandler(ITechnologyCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(PublishTechnologyCategoryCommand request, CancellationToken cancellationToken)
    {
        await _repository.PublishAsync(request.Id);
        return Unit.Value;
    }
}