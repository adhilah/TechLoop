using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.Technologies.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;
using TechLoop.Domain.Entities;
using TechLoop.Domain.Enums;
using MediatR;

namespace TechLoop.Application.Features.Technologies.Commands.CreateTechnology;

public sealed class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreateTechnologyResponse>
{
    private readonly ITechnologyRepository _technologyRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly ICategoryRepository _categoryRepository;

    public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
    {
        _technologyRepository = technologyRepository;
        _categoryRepository = categoryRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CreateTechnologyResponse> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
    {
        var exists = await _technologyRepository.ExistsAsync(request.CategoryId, request.Name, cancellationToken);
        if (exists)
        {
            throw new ValidationException($"Technology '{request.Name}' already exists in the category.");
        }
        
        var slugExists = await _technologyRepository.SlugExistsAsync(request.Slug, cancellationToken);
        if (slugExists)
        {
            throw new ValidationException($"Technology slug '{request.Slug}' already exists.");
        }
        var positionExists = await _technologyRepository.PositionExistsAsync(request.CategoryId,request.Position, cancellationToken);
        if (positionExists)
        {
            throw new ValidationException($"Technology position '{request.Position}' already exists in the category.");
        }
        var categoryExists = await _categoryRepository.ExistsAsync(request.CategoryId, cancellationToken);
        if (!categoryExists)
        {
            throw new NotFoundException("Category not found.");
        }
        
        var technology = new Technology
        {
            CategoryId = request.CategoryId,
            Name = request.Name.Trim(),
            Slug = request.Slug.Trim().ToLowerInvariant(),
            Description = request.Description ?? string.Empty,
            ImageUrl = request.ImageUrl ?? string.Empty,
            Position = request.Position,
            CreatedBy = _currentUserService.UserId,
            CreatedAt = DateTime.UtcNow
        };
        var id = await _technologyRepository.CreateAsync(technology, cancellationToken);
        
        var createdTechnology = await _technologyRepository.GetByIdAsync(id, cancellationToken);

        if (createdTechnology is null)
            throw new Exception("Failed to create technology.");

        return new CreateTechnologyResponse
        {
            Success = true,
            Message = "Technology added successfully."
        };
    }

    private static string GenerateSlug(string value)
    {
        return value
            .Trim()
            .ToLowerInvariant()
            .Replace(" ", "-")
            .Replace("--", "-");
    }
}