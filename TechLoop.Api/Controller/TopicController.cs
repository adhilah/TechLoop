using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechLoop.Application.Features.Topics.DTOs;
using TechLoop.Application.Features.Topics.Queries.GetAllTopics.Learner;
using TechLoop.Application.Features.Topics.Queries.GetTopicById.Learner;

namespace TechLoop.Api.Controllers;

[ApiController]
[Route("topics")]
public sealed class TopicController : ControllerBase
{
    private readonly IMediator _mediator;

    public TopicController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET /topics
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LearnerTopicResponse>>> GetAllTopics(
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetAllLearnerTopicsQuery(),
            cancellationToken);

        return Ok(result);
    }

    // GET /topics/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LearnerTopicResponse>> GetTopicById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetLearnerTopicByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }
}
