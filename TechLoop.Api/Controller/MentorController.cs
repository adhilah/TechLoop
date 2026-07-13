using TechLoop.Application.Features.Technologies.Commands.CreateTechnology;
using TechLoop.Application.Features.Technologies.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLoop.Application.DTOs.SubTopics.Requests;
using TechLoop.Application.Features.SubTopics.Commands.CreateSubTopic;
using TechLoop.Application.Features.SubTopics.Commands.DeleteSubTopic;
using TechLoop.Application.Features.SubTopics.Commands.UpdateSubTopic;
using TechLoop.Application.Features.SubTopics.DTOs;
using TechLoop.Application.Features.Technologies.Commands.DeleteTechnology;
using TechLoop.Application.Features.Technologies.Commands.UpdateTechnology;
using TechLoop.Application.Features.Topics.Commands.CreateTopic;
using TechLoop.Application.Features.Topics.Commands.DeleteTopic;
using TechLoop.Application.Features.Topics.Commands.UpdateTopic;
using TechLoop.Application.Features.Topics.DTOs;
using TechLoop.Application.DTOs.Questions.Requests;
using TechLoop.Application.Features.Questions.Commands.CreateQuestion;
using TechLoop.Application.Features.Questions.Commands.UpdateQuestion;
using TechLoop.Application.Features.Questions.Commands.DeleteQuestion;
using TechLoop.Application.Features.Questions.DTOs;
using TechLoop.Application.Features.Questions.Queries.GetAllMentorQuestions;
using TechLoop.Application.Features.Questions.Queries.GetMentorQuestionById;
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
            request.Position,
            request.Status);

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
            request.Position);

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
    
    //Soft Delete Topic
    [HttpDelete("topics/{id:int}")]
    public async Task<ActionResult<DeleteTopicResponse>> DeleteTopic(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteTopicCommand(id);

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
    
    //create subtop
    [HttpPost("subtopics")]
    public async Task<ActionResult<CreateSubTopicResponse>> CreateSubTopic(
        [FromBody] CreateSubTopicRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateSubTopicCommand(
            request.TopicId,
            request.Title,
            request.Description,
            request.ImageUrl,
            request.Slug,
            request.Position,
            request.Status);

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
    
    // Update Subtopic
    [HttpPut("subtopics/{id:int}")]
    public async Task<ActionResult<UpdateSubTopicResponse>> UpdateSubTopic(int id,
        [FromBody] UpdateSubTopicRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateSubTopicCommand(
            id,
            request.TopicId,
            request.Title,
            request.Description,
            request.ImageUrl,
            request.Slug,
            request.Position);

        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    // Soft delete Subtopic
    [HttpDelete("subtopics/{id:int}")]
    public async Task<ActionResult<DeleteSubTopicResponse>> DeleteSubTopic(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteSubTopicCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    // Create Question
    [HttpPost("questions")]
    public async Task<ActionResult<CreateQuestionResponse>> CreateQuestion(
        [FromBody] CreateQuestionRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateQuestionCommand(
            request.SubTopicId,
            request.QuestionType,
            request.Title,
            request.Slug,
            request.Description,
            request.ImageUrl,
            request.Mark,
            request.Hint,
            request.Explanation,
            request.TimeLimitSeconds,
            request.MemoryLimitMb,
            request.Difficulty,
            request.Position,
            request.Status);

        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

// Update Question
    [HttpPut("questions/{id:int}")]
    public async Task<ActionResult<UpdateQuestionResponse>> UpdateQuestion(int id,
        [FromBody] UpdateQuestionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateQuestionCommand(
            id,
            request.SubTopicId,
            request.QuestionType,
            request.Title,
            request.Slug,
            request.Description,
            request.ImageUrl,
            request.Mark,
            request.Hint,
            request.Explanation,
            request.TimeLimitSeconds,
            request.MemoryLimitMb,
            request.Difficulty,
            request.Position);

        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

// Soft Delete Question
    [HttpDelete("questions/{id:int}")]
    public async Task<ActionResult<DeleteQuestionResponse>> DeleteQuestion(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteQuestionCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    // Get all questions
    [HttpGet("questions")]
    public async Task<ActionResult<IEnumerable<MentorQuestionResponse>>> GetAllQuestions(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllMentorQuestionsQuery(), cancellationToken);
        return Ok(result);
    } 
    
// Get question by id
    [HttpGet("questions/{id:int}")]
    public async Task<ActionResult<MentorQuestionResponse>> GetQuestionById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetMentorQuestionByIdQuery(id), cancellationToken);
        return Ok(result);
    }}