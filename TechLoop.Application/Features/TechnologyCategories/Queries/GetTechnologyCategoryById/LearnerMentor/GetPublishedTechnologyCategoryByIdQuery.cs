using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;

namespace TechLoop.Application.Features.TechnologyCategories.Queries.GetTechnologyCategoriesById.LearnerMentor;

public sealed record GetPublishedTechnologyCategoriesByIdQuery(int Id) : IRequest<LearnerMentorTechnologyCategoryResponse?>;