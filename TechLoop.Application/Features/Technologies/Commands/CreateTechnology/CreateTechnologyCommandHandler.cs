using TechLoop.Application.Features.Technologies.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;
using TechLoop.Domain.Entities;
using MediatR;

namespace TechLoop.Application.Features.Technologies.Commands.CreateTechnology;

public sealed class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreateTechnologyResponse>
{
    private readonly ITechnologyRepository _technologyRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateTechnologyCommandHandler(
        ITechnologyRepository technologyRepository,
        ICurrentUserService currentUserService)
    {
        _technologyRepository = technologyRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CreateTechnologyResponse> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
    {
        var exists = await _technologyRepository.ExistsAsync(
            request.Name,
            cancellationToken);

        if (exists)
            throw new InvalidOperationException(
                $"Technology '{request.Name}' already exists.");
        var technology = new Technology
        {
            CategoryId = request.CategoryId,
            Name = request.Name.Trim(),
            Slug = GenerateSlug(request.Name),
            Description = request.Description ?? string.Empty,
            ImageUrl = request.ImageUrl ?? string.Empty,
            Position = request.Position,
            Status = "Draft",
            PublishedAt = null,
            PublishedBy = null,
            CreatedBy = _currentUserService.UserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedBy = _currentUserService.UserId,
            UpdatedAt = DateTime.UtcNow
        };
        var id = await _technologyRepository.CreateAsync(
            technology,
            cancellationToken);
        
        var createdTechnology =
            await _technologyRepository.GetByIdAsync(
                id,
                cancellationToken);

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