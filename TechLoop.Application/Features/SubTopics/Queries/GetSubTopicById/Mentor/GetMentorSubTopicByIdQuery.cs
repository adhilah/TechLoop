using MediatR;
using TechLoop.Application.Features.SubTopics.DTOs;

namespace TechLoop.Application.Features.SubTopics.Queries.GetSubTopicById.Mentor;

public sealed record GetMentorSubTopicByIdQuery(int Id ) : IRequest<MentorSubTopicResponse>;