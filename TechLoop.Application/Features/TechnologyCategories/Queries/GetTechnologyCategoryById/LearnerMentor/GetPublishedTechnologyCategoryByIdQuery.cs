using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;

namespace TechLoop.Application.Features.TechnologyCategories.Queries.GetTechnologyCategoryById.LearnerMentor;

public sealed record GetPublishedTechnologyCategoryByIdQuery(int Id) : IRequest<LearnerMentorTechnologyCategoryResponse?>;