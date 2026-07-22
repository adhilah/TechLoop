using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Application.Features.TechnologyCategories.Queries.GetAllTechnologyCategories.LearnerMentor;

public sealed class GetAllPublishedTechnologyCategoriesQueryHandler : IRequestHandler<GetAllPublishedTechnologyCategoriesQuery, IEnumerable<LearnerMentorTechnologyCategoryResponse>>
{
    private readonly ITechnologyCategoryRepository _repository;
    public GetAllPublishedTechnologyCategoriesQueryHandler(ITechnologyCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LearnerMentorTechnologyCategoryResponse>> Handle(GetAllPublishedTechnologyCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllForPublicAsync();
    }
}