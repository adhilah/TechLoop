using TechLoop.Application.Features.Technologies.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLoop.Application.Features.Technologies.Queries.GetTechnologyById;
using TechLoop.Application.Features.Technologies.Queries.GetAllTechnologies;

[ApiController]
[Route("technologies")]
public sealed class TechnologyController : ControllerBase
{
    private readonly IMediator _mediator;

    public TechnologyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllTechnologiesQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTechnologyByIdQuery(id), cancellationToken);

        return Ok(result);
    }
}