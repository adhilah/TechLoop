using MediatR;
using TechLoop.Application.Features.Topics.DTOs;

namespace TechLoop.Application.Features.Topics.Queries.GetTopicById.Mentor;

public sealed record GetMentorTopicByIdQuery(int Id ) : IRequest<MentorTopicResponse>;