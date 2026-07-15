using MediatR;
using TechLoop.Application.Features.Technologies.DTOs;

namespace TechLoop.Application.Features.Technologies.Queries.GetAllTechnologies.Mentor;

public sealed record GetAllMentorTechnologiesQuery : IRequest<IEnumerable<MentorTechnologyResponse>>;