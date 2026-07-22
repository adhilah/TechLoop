using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.TechnologyCategories.Queries.GetAllTechnologyCategories.Admin;

public sealed class GetAllTechnologyCategoriesQueryHandler : IRequestHandler<GetAllTechnologyCategoriesQuery, IEnumerable<AdminTechnologyCategoryResponse>>
{
    private readonly ITechnologyCategoryRepository _repository;

    public GetAllTechnologyCategoriesQueryHandler(ITechnologyCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AdminTechnologyCategoryResponse>> Handle(GetAllTechnologyCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllForAdminAsync();
    }
}