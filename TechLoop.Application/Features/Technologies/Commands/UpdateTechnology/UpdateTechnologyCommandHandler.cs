using TechLoop.Application.Features.Technologies.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;
using MediatR;
using TechLoop.Application.Common.Exceptions;


namespace TechLoop.Application.Features.Technologies.Commands.UpdateTechnology;

public sealed class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdateTechnologyResponse>
{
   private readonly ITechnologyRepository _technologyRepository;
   private readonly ICurrentUserService _currentUserService;
   private readonly ICategoryRepository _categoryRepository;

   public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository, ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
   {
      _technologyRepository = technologyRepository;
      _categoryRepository = categoryRepository;
      _currentUserService = currentUserService;
   }

   public async Task<UpdateTechnologyResponse> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
   {
      var technology = await _technologyRepository.GetByIdAsync(request.id, cancellationToken);
      if (technology is null)
      {
         throw new NotFoundException(("Technology not found"));
      }
      var exists = await _technologyRepository.ExistsAsync(request.Name, cancellationToken);

      if (exists && !technology.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase))
      {
         throw new ValidationException($"Technology '{request.Name}' already exists.");
      }
      
      var categoryExists = await _categoryRepository.ExistsAsync(
         request.CategoryId,
         cancellationToken);

      if (!categoryExists)
      {
         throw new NotFoundException("Category not found.");
      }

      technology.CategoryId = request.CategoryId;
      technology.Name =request.Name.Trim();
      technology.Description = request.Description;
      technology.Slug = request.Slug;
      technology.ImageUrl = request.ImageUrl;
      technology.Position = request.Position;
      technology.UpdatedAt = DateTime.UtcNow;
      technology.UpdatedBy = _currentUserService.UserId;

      var result = await _technologyRepository.UpdateAsync(technology, cancellationToken);

      if (result <= 0)
      {
         throw new Exception("Technology update failed.");
      }

      return new UpdateTechnologyResponse
      {
         Success = true,
         Message = "Technology updated successfully."
      };
   }
}

