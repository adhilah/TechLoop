using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Domain.Entities;

namespace TechLoop.Application.Features.TechnologyCategories.Commands.UpdateTechnologyCategories;

public sealed class UpdateTechnologyCategoryCommandHandler : IRequestHandler<UpdateTechnologyCategoryCommand, UpdateTechnologyCategoryResponse>
{
    private readonly ITechnologyCategoryRepository _repository;

    public UpdateTechnologyCategoryCommandHandler(ITechnologyCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateTechnologyCategoryResponse> Handle(UpdateTechnologyCategoryCommand request, CancellationToken cancellationToken)
    {
        var technologyCategory = new TechnologyCategory
        {
            Id = request.Request.Id,
            Name = request.Request.Name,
            UpdatedBy = request.UpdatedBy
        };

        await _repository.UpdateAsync(technologyCategory);
        return new UpdateTechnologyCategoryResponse
        {
            Id = technologyCategory.Id,
            Name = technologyCategory.Name
        };
    }
}