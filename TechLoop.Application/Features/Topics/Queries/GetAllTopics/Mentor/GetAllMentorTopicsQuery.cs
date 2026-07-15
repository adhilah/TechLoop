using MediatR;
using TechLoop.Application.Features.Topics.DTOs;

namespace TechLoop.Application.Features.Topics.Queries.GetAllTopics.Mentor;

public sealed record GetAllMentorTopicsQuery : IRequest<IEnumerable<MentorTopicResponse>>;