using MediatR;
using TechLoop.Application.Features.Technologies.DTOs;

namespace TechLoop.Application.Features.Technologies.Queries.GetTechnologyById.Mentor;

public sealed record GetMentorTechnologyByIdQuery(int Id ) : IRequest<MentorTechnologyResponse>;