using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLoop.Application.Features.Topics.DTOs;
using TechLoop.Application.Features.Topics.Queries.GetAllTopics;
using TechLoop.Application.Features.Topics.Queries.GetTopicById;

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
    
    //Get all topics
    [AllowAnonymous]
    [HttpGet("topics")]
    public async Task<ActionResult<IEnumerable<TopicResponse>>> GetAllTopics(
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetAllTopicQuery(),
            cancellationToken);

        return Ok(result);
    }

    //get topic by id
    [AllowAnonymous]
    [HttpGet("topics/{id:int}")]
    public async Task<ActionResult<TopicResponse>> GetTopicById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetTopicByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }
}
