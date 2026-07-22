using FluentValidation;
using MediatR;
using TechLoop.Application.Features.TechnologyCategories.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Domain.Entities;

namespace TechLoop.Application.Features.TechnologyCategories.Commands.CreateTechnologyCategory;

public sealed class CreateTechnologyCategoriesCommandHandler : IRequestHandler<CreateTechnologyCategoriesCommand, CreateTechnologyCategoryResponse>
{
    private readonly ITechnologyCategoryRepository _repository;
    public CreateTechnologyCategoriesCommandHandler(ITechnologyCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateTechnologyCategoryResponse> Handle(CreateTechnologyCategoriesCommand request, CancellationToken cancellationToken)
    {
        var exists = await _repository.NameExistsAsync(request.Request.Name, null, cancellationToken);
        if (exists)
        {
            throw new ValidationException($"Technology category '{request.Request.Name}' already exists.");
        }

        var technologyCategory = new TechnologyCategory
        {
            Name = request.Request.Name.Trim(),
            CreatedBy = request.CreatedBy
        };

        await _repository.CreateAsync(technologyCategory, cancellationToken);
        return new CreateTechnologyCategoryResponse
        {
            Success = true,
            Message = "Technology category created successfully."
        };
    }
}