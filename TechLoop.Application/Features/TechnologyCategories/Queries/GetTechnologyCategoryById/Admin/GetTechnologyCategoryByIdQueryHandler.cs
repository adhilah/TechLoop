using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.TechnologyCategories.Queries.GetTechnologyCategoriesById.Admin;

public sealed class GetTechnologyCategoriesByIdQueryHandler : IRequestHandler<GetTechnologyCategoryByIdQuery, AdminTechnologyCategoryResponse?>
{
    private readonly ITechnologyCategoryRepository _repository;
    public GetTechnologyCategoriesByIdQueryHandler(ITechnologyCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<AdminTechnologyCategoryResponse?> Handle(GetTechnologyCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdForAdminAsync(request.Id);
    }
}