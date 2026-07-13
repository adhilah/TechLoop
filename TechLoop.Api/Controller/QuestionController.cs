using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLoop.Application.Features.Questions.DTOs;
using TechLoop.Application.Features.Questions.Queries.GetAllLearnerQuestions;
using TechLoop.Application.Features.Questions.Queries.GetAllQuestions.Learner;
using TechLoop.Application.Features.Questions.Queries.GetLearnerQuestionById;

namespace TechLoop.Api.Controllers;

[Authorize(Policy = "LearnerOnly")]
[ApiController]
[Route("questions")]
public sealed class QuestionController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuestionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: questions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LearnerQuestionResponse>>> GetAllQuestions(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllLearnerQuestionsQuery(), cancellationToken);
        return Ok(result);
    }

    // GET: questions/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LearnerQuestionResponse>> GetQuestionById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLearnerQuestionByIdQuery(id), cancellationToken);
        return Ok(result);
    }
}