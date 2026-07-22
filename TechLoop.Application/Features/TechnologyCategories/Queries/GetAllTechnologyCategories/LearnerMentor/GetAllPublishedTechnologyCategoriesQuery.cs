using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;

namespace TechLoop.Application.Features.TechnologyCategories.Queries.GetAllTechnologyCategories.LearnerMentor;

public sealed record GetAllPublishedTechnologyCategoriesQuery : IRequest<IEnumerable<LearnerMentorTechnologyCategoryResponse>>;