using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechLoop.Application.Features.TechnologyCategories.Commands.CreateTechnologyCategory;
using TechLoop.Application.Features.TechnologyCategories.Commands.DeleteTechnologyCategory;
using TechLoop.Application.Features.TechnologyCategories.Commands.PublishTechnologyCategory;
using TechLoop.Application.Features.TechnologyCategories.Commands.UpdateTechnologyCategory;
using TechLoop.Application.Features.TechnologyCategories.DTOs;
using TechLoop.Application.Features.TechnologyCategories.Queries.GetAllTechnologyCategories.Admin;
using TechLoop.Application.Features.TechnologyCategories.Queries.GetTechnologyCategoryById.Admin;

namespace TechLoop.Api.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/admin")]
[ApiController]
public sealed class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //Create Category
    [HttpPost("technology-categories")]
    public async Task<IActionResult> CreateTechnologyCategory([FromBody] CreateTechnologyCategoryRequest request)
    {
        var createdBy = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _mediator.Send(new CreateTechnologyCategoriesCommand(request, createdBy));
        return Ok(result);
    }

    //Update Category
    [HttpPut("technology-categories/{id:int}")]
    public async Task<IActionResult> UpdateTechnologyCategory(int id, [FromBody] UpdateTechnologyCategoryRequest request)
    {
        var updatedBy = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _mediator.Send(new UpdateTechnologyCategoryCommand(id, request, updatedBy));
        return Ok(result);
    }

    //Update Publish
    [HttpPatch("technology-categories/{id:int}/publish")]
    public async Task<IActionResult> PublishTechnologyCategory(int id)
    {
        var result = await _mediator.Send(new PublishTechnologyCategoryCommand(id));
        return Ok(result);
    }

    //Soft Delete
    [HttpDelete("technology-categories/{id:int}")]
    public async Task<IActionResult> DeleteTechnologyCategory(int id)
    {
        var deletedBy = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _mediator.Send(new DeleteTechnologyCategoryCommand(id, deletedBy));
        return Ok(result);
    }
    
    //Get all category
    [HttpGet("technology-categories")]
    public async Task<IActionResult> GetAllTechnologyCategories()
    {
        var result = await _mediator.Send(new GetAllTechnologyCategoriesQuery());
        return Ok(result);
    }

    //Get all category by id
    [HttpGet("technology-categories/{id:int}")]
    public async Task<IActionResult> GetTechnologyCategoryById(int id)
    {
        var result = await _mediator.Send(new GetTechnologyCategoryByIdQuery(id));
        if (result is null)
            return NotFound();

        return Ok(result);
    }
}