using TechLoop.Application.Features.Technologies.Commands.CreateTechnology;
using TechLoop.Application.Features.Technologies.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLoop.Application.Features.Technologies.Commands.DeleteTechnology;
using TechLoop.Application.Features.Technologies.Commands.UpdateTechnology;
using TechLoop.Application.Features.Topics.Commands.CreateTopic;
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

    // Technology APIs

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

    [HttpDelete("technologies/{id:int}")]
    public async Task<IActionResult> DeleteTechnology(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteTechnologyCommand(id);

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    // Topic APIs

    [HttpPost("topics")]
    public async Task<ActionResult<CreateTopicResponse>> CreateTopic(
        [FromBody] CreateTopicCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}