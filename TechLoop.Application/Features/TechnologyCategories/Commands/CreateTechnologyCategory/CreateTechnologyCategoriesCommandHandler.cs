using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Domain.Entities;

namespace TechLoop.Application.Features.TechnologyCategories.Commands.CreateTechnologyCategories;

public sealed class CreateTechnologyCategoriesCommandHandler : IRequestHandler<CreateTechnologyCategoriesCommand, CreateTechnologyCategoryResponse>
{
    private readonly ITechnologyCategoryRepository _repository;
    public CreateTechnologyCategoriesCommandHandler(ITechnologyCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateTechnologyCategoryResponse> Handle( CreateTechnologyCategoriesCommand request, CancellationToken cancellationToken)
    {
        var technologyCategory = new TechnologyCategory
        {
            Name = request.Request.Name,
            CreatedBy = request.CreatedBy
        };

        await _repository.CreateAsync(technologyCategory);
        return new CreateTechnologyCategoryResponse
        {
            Name = technologyCategory.Name
        };
    }
}