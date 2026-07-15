using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechLoop.Application.Features.Technologies.DTOs;
using TechLoop.Application.Features.Technologies.Queries.GetAllTechnologies.Learner;
using TechLoop.Application.Features.Technologies.Queries.GetTechnologyById.Learner;

namespace TechLoop.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<LearnerTechnologyResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllLearnerTechnologiesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LearnerTechnologyResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLearnerTechnologyByIdQuery(id), cancellationToken);
        return Ok(result);
    }
}