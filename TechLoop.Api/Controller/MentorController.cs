using TechLoop.Application.Features.Technologies.Commands.CreateTechnology;
using TechLoop.Application.Features.Technologies.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLoop.Application.Features.Technologies.Commands.DeleteTechnology;
using TechLoop.Application.Features.Technologies.Commands.UpdateTechnology;
using TechLoop.Application.Features.Topics.Commands.CreateTopic;
using TechLoop.Application.Features.Topics.Commands.DeleteTopic;
using TechLoop.Application.Features.Topics.Commands.UpdateTopic;
using TechLoop.Application.Features.Topics.DTOs;

//using System.Security.Claims;

namespace TechLoop.Api.Controllers;

[Authorize(Policy = "MentorOnly")]
[ApiController]
[Route("mentor")]
public sealed class MentorController : ControllerBase
{
    private readonly IMediator _mediator;

    public MentorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Create Technology

    [HttpPost("technologies")]
    public async Task<ActionResult<CreateTechnologyResponse>> CreateTechnology(
        [FromBody] CreateTechnologyRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateTechnologyCommand(
            request.CategoryId,
            request.Name,
            request.Description,
            request.Slug,
            request.ImageUrl,
            request.Position);

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    
    //Update Technology
    [HttpPut("technologies/{id:int}")]
    public async Task<ActionResult<UpdateTechnologyResponse>> UpdateTechnology(
        int id,
        [FromBody] UpdateTechnologyRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateTechnologyCommand(
            id,
            request.CategoryId,
            request.Name,
            request.Description,
            request.Slug,
            request.ImageUrl,
            request.Position);

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
    
    
    //Soft Delete Technology
    [HttpDelete("technologies/{id:int}")]
    public async Task<IActionResult> DeleteTechnology(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteTechnologyCommand(id);

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    
    // Create Topic
    [HttpPost("topics")]
    public async Task<ActionResult<CreateTopicResponse>> CreateTopic(
        [FromBody] CreateTopicCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
    
    //Update Topic
    [HttpPut("topics/{id:int}")]
    public async Task<ActionResult<UpdateTopicResponse>> UpdateTopic(int id,
        [FromBody] UpdateTopicRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdatedTopicCommand(
            id,
            request.TechnologyId,
            request.Title,
            request.Description,
            request.ImageUrl,
            request.Slug,
            request.Position,
            request.Status);

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
    
    //Delete Topic
    [HttpDelete("topics/{id:int}")]
    public async Task<ActionResult<DeleteTopicResponse>> DeleteTopic(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteTopicCommand(id);

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}