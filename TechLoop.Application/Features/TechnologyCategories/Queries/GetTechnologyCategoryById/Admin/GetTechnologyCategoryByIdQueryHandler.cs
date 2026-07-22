using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.TechnologyCategories.Queries.GetTechnologyCategoryById.Admin;

public sealed class GetTechnologyCategoryByIdQueryHandler : IRequestHandler<GetTechnologyCategoryByIdQuery, AdminTechnologyCategoryResponse?>
{
    private readonly ITechnologyCategoryRepository _repository;
    public GetTechnologyCategoryByIdQueryHandler(ITechnologyCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<AdminTechnologyCategoryResponse?> Handle(GetTechnologyCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdForAdminAsync(request.Id, cancellationToken);
    }
}