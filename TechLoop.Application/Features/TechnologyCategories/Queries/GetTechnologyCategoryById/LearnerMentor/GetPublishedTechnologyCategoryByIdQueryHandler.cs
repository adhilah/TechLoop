using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.TechnologyCategories.Queries.GetTechnologyCategoriesById.LearnerMentor;

public sealed class GetPublishedTechnologyCategoriesByIdQueryHandler : IRequestHandler<GetPublishedTechnologyCategoryByIdQuery, LearnerMentorTechnologyCategoryResponse?>
{
    private readonly ITechnologyCategoryRepository _repository;
    public GetPublishedTechnologyCategoriesByIdQueryHandler(ITechnologyCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<LearnerMentorTechnologyCategoryResponse?> Handle(GetPublishedTechnologyCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdForPublicAsync(request.Id);
    }
}