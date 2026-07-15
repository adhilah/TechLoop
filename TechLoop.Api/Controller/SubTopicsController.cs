using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechLoop.Application.Features.SubTopics.DTOs;
using TechLoop.Application.Features.SubTopics.Queries.GetAllSubTopics.Learner;
using TechLoop.Application.Features.SubTopics.Queries.GetSubTopicById.Learner;

namespace TechLoop.Api.Controllers;

[ApiController]
[Route("subtopics")]
public sealed class SubTopicController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubTopicController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Get all subtopics
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LearnerSubTopicResponse>>> GetAllSubTopics(
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllLearnerSubTopicsQuery(), cancellationToken);
        return Ok(result);
    }

    // Get subtopic by id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LearnerSubTopicResponse>> GetSubTopicById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLearnerSubTopicByIdQuery(id), cancellationToken);
        return Ok(result);
    }
}