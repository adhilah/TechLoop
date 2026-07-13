using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechLoop.Application.Features.SubTopics.DTOs;
using TechLoop.Application.Features.SubTopics.Queries.GetAllSubTopics;
using TechLoop.Application.Features.SubTopics.Queries.GetSubTopicById;

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
    public async Task<ActionResult<SubTopicResponse>> GetAllSubTopics(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllSubTopicsQuery(), cancellationToken);

        return Ok(result);
    }

    // Get subtopic by id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SubTopicResponse>> GetSubTopicById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetSubTopicByIdQuery(id), cancellationToken);

        return Ok(result);
    }
}